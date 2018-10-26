using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Laboratory56.Models
{
    public class CommentVM
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int PostId { get; set; }
        public Publication Post { get; set; }
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }
        public IFormFile ImageUrl { get; set; }
    }
}