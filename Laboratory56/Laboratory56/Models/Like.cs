using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratory56.Models
{
    public class Like
    {
        [Key]
        public int  LikeId { get; set; }

        [Display(Name = "Нравиться!")] public int LikeProperty { get; set; }
    }
}
