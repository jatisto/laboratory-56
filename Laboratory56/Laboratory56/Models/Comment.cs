using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratory56.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public string PostId { get; set; }
        public Publication Post { get; set; }

        public DateTime CommentDate { get; set; }

    }
}
