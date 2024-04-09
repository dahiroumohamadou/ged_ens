using soft.Models;

namespace soft.FileUploadService
{
    public interface IFileUploadService
    {
        Task<string> UploadFileAsync(IFormFile file, Membre m);
    }
}
