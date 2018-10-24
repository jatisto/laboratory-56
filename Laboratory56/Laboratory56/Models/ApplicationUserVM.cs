using Microsoft.AspNetCore.Http;

namespace Laboratory56.Models
{
    public class ApplicationUserVM
    {
        public IFormFile AvatarImage { get; set; } //IFormFile Хранит в себе картинку  
    }
}