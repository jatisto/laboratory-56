using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Laboratory56.Models
{
    public class PublicationVM
    {
        public IFormFile ImageUrl { get; set; }
        public string Description { get; set; }
        public int Like { get; set; }
        public int RePost { get; set; }
    }
}
