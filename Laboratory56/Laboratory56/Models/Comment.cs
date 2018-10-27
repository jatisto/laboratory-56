using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratory56.Models
{
    public class Comment
    {
        [Key] public int CommentId { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int PostId { get; set; }
        public Publication Post { get; set; }
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }

        [Display(Name = "Изображение")] public string ImageUrl { get; set; }
    }
}
