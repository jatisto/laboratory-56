using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratory56.Models
{
    public class Subscription
    {
        public int Id { get; set; }

        public string UserIdSub { get; set; }
        public ApplicationUser UserSub { get; set; }

        public int PostIdSub { get; set; }
        public Publication PostSub { get; set; }

        [Display(Name = "Подписка!")] public int SubscriptionCount { get; set; }
    }
}