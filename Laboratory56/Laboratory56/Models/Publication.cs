using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratory56.Models
{
    public class Publication
    {
        public int Id { get; set; }
        [Display(Name = "Изображение")]
        public string ImageUrl { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Нравиться!")]
        public int Like { get; set; }
    }
}