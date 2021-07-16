using System.Collections.Generic;
using TuHobbyWeb.Models.Entities;

namespace TuHobbyWeb.Models.ViewModels
{
    public class ProductIndexViewModel
    {
        public List<Product> Products { get; set; }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public string Search { get; set; }

        public int? PlatformId { get; set; } // Por defecto NULL

        public int Sort { get; set; } // Por defecto es 0

        public List<ProductPlatform> Platforms { get; set; }
    }
}