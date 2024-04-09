using soft.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace soft.FileUploadService
{
    public class LocalFileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _environment;
        public LocalFileUploadService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public async Task<string> UploadFileAsync(IFormFile file, Membre m)
        {
            // resize image 
            var filePath = Path.Combine(_environment.ContentRootPath, "wwwroot/img/photos", m.Matricule + ".png");
            using var image=Image.Load(file.OpenReadStream());
            image.Mutate(x => x.Resize(180,180));
            image.Save(filePath);
            // convert and copy
            //var filePath = Path.Combine(_environment.ContentRootPath, "wwwroot/img/photos", m.Matricule+".png");
            //using var fileStream = new FileStream(filePath, FileMode.Create);
            //await file.CopyToAsync(fileStream);
            return filePath;
        }
    }
}
