using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratory56.Models
{
    public class Comment
    {
        [Key] public string CommentId { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string PostId { get; set; }
        public Publication Post { get; set; }
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }

        [Display(Name = "Количество  комментариев")]
        public int ComentCount { get; set; }

    }
}
