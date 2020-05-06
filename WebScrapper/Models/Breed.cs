using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapper.Models
{
    public class Breed
    {
        public int Id { get; set; }

        //  Profile
        public string Name { get; set; }
        public string Category { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string LifeSpan { get; set; }
        public string Summary { get; set; }

        //  Image
        public string ImageUrl { get; set; }
    }
}
