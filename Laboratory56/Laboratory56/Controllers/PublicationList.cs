using System.ComponentModel.DataAnnotations;

namespace Laboratory56.Controllers
{
    internal class PublicationList
    {
        public int Id { get; set; }
        [Display(Name = "Изображение")]
        public string ImageUrl { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Нравиться!")]
        public int Like { get; set; }
        [Display(Name = "Колличество комментариев")]
        public int ComentCount { get; set; }
    }
}