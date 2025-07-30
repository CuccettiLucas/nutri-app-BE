using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace App_Nutri.Services
{
    public interface IStorageService
    {
        Task<string> SubirImg(IFormFile archivo);
        Task<bool> EliminarImagenAsync(string url);
    }
}
