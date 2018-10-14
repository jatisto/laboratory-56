using System.IO;
using Microsoft.AspNetCore.Http;

namespace Laboratory56.Services
{
    public class FileUploadService
    {
        public FileUploadService()
        {
            
        }
        public async void Upload(string path, string fileName, IFormFile file)
        {
            Directory.CreateDirectory(path);
            using (var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }
    }
}