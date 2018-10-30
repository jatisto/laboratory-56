using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratory56.Models
{
    public class Publication
    {
        public int Id { get; set; }

        [Display(Name = "Изображение")] public string ImageUrl { get; set; }
        [Display(Name = "Описание")] public string Description { get; set; }
        [Display(Name = "Нравиться!")] public int Like { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        /*public string PublicSubId { get; set; }
        public Subscription PublicSub { get; set; }*/

        [Display(Name = "Количество  комментариев")]
        public int ComentCount { get; set; }

        public List<Comment> CommentsList { get; set; }
//        public List<Subscription> SubscriptionsList { get; set; }

    }
}