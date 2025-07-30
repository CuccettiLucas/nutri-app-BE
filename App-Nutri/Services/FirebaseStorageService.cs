using App_Nutri.Services;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

public class FirebaseStorageService : IStorageService
{
    private readonly StorageClient _storageClient;
    private readonly string _bucketName = "nutri-app-storage.firebasestorage.app";

    public FirebaseStorageService(IConfiguration configuration)
    {
        var credentialPath = configuration["Firebase:CredentialPath"];
        var credential = GoogleCredential.FromFile(credentialPath);
        _storageClient = StorageClient.Create(credential);
    }

    public async Task<string> SubirImg(IFormFile archivo)
    {
        if (archivo == null || archivo.Length == 0)
            throw new Exception("Archivo no válido");

        var nombreUnico = $"{Guid.NewGuid()}{Path.GetExtension(archivo.FileName)}";
        var objectName = $"imagenes/{nombreUnico}";

        using var stream = archivo.OpenReadStream();

        var obj = await _storageClient.UploadObjectAsync(new Google.Apis.Storage.v1.Data.Object
        {
            Bucket = _bucketName,
            Name = objectName,
        }, stream, new UploadObjectOptions { PredefinedAcl = PredefinedObjectAcl.PublicRead });

        return $"https://storage.googleapis.com/{_bucketName}/{objectName}";
    }

    public async Task<bool> EliminarImagenAsync(string url)
    {
        try
        {
            // Ejemplo de URL:
            // https://storage.googleapis.com/nutri-app-storage.firebasestorage.app/imagenes/archivo.jpg

            var baseUrl = $"https://storage.googleapis.com/{_bucketName}/";
            if (!url.StartsWith(baseUrl))
                throw new Exception("La URL no pertenece al bucket configurado.");

            // Extraer nombre del objeto
            var objectName = url.Substring(baseUrl.Length);

            // Eliminar objeto del bucket
            await _storageClient.DeleteObjectAsync(_bucketName, objectName);

            return true;
        }
        catch (Google.GoogleApiException ex) when (ex.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
        {
            // Ya está eliminado o nunca existió
            return false;
        }
        catch (Exception ex)
        {
            // Puedes loguear el error si lo necesitas
            throw new Exception("Error al eliminar la imagen: " + ex.Message);
        }
    }

}
