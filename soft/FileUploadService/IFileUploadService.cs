using ged.Models;
using soft.Models;

namespace soft.FileUploadService
{
    public interface IFileUploadService
    {
        //Task<string> UploadPdfFileAsync(IFormFile file, Dossier d);
        Task<string> UploadPdfFileArreteAsync(IFormFile file, Doc a);
        Task<string> UploadPdfFileCommuniqueAsync(IFormFile file, Doc c);
        Task<string> UploadPdfFilePvsAsync(IFormFile file, Doc pv);
        Task<string> UploadPdfFileAutreAsync(IFormFile file, Doc a);
    }
}
