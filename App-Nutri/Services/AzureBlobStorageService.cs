using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace App_Nutri.Services
{
    public class AzureBlobStorageService : IStorageService
    {
        private readonly string conectionString = "DefaultEndpointsProtocol=https;AccountName=nutriapp777;AccountKey=yi3oupezJR4JbP5Er77dWEQHVpI+7ct84DL7Kzz0mSsy/gmbSNQ/gEYUwmHLdTPE2OA8UgUqzlay+AStF+/nhg==;EndpointSuffix=core.windows.net";
        private readonly string containerName = "imagenes";

        private readonly BlobServiceClient _blobServiceClient;

        public AzureBlobStorageService()
        {
            _blobServiceClient = new BlobServiceClient(conectionString);
        }

        public async Task<string> SubirImg(IFormFile archivo)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(archivo.FileName)}";
            var blobClient = blobContainerClient.GetBlobClient(fileName);

            using var stream = archivo.OpenReadStream();
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = archivo.ContentType });

            return blobClient.Uri.ToString();

        }

        public async Task<bool> EliminarImagenAsync(string url)
        {
            try
            {
                var fileName = url.Split('/').Last();
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                var blobClient = blobContainerClient.GetBlobClient(fileName);

                await blobClient.DeleteIfExistsAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
