using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Laboratory56.Models
{
    public class PublicationVM
    {
        [Display(Name = "Изображение")]
        public IFormFile ImageUrl { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        public int Like { get; set; }
        public int RePost { get; set; }
        public string ImageUrlPath { get; set; }
    }
}
