using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratory56.Models
{
    public class Subscription
    {
        [Key] public int SubscriptionId { get; set; }

        public string SubscribersId { get; set; }
        public ApplicationUser Subscribers { get; set; } // тот на кого подписались

        public string SubscribedId { get; set; }
        public ApplicationUser Subscribed { get; set; } // Тот кто подписалься

        [Display(Name = "Количество подписчиков!")]
        public int SubCount { get; set; }
    }
}