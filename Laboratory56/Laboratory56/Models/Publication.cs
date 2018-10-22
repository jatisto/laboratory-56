using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratory56.Models
{
    public class Publication
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }

        public int Like { get; set; }
        public int RePost { get; set; }
    }
}