using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Labarotory_56.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string AvatarImage { get; set; }
        public string Name { get; set; }
        public int Publications { get; set; }
        public int Subscriptions { get; set; }
    }
}
