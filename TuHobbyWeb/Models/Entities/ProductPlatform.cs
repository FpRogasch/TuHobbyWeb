using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace TuHobbyWeb.Models.Entities
{
    public class ProductPlatform
    {
        [Key]
        public int ProductPlatformId { get; set; }

        [Required]
        public string ProductPlatformName { get; set; }

        [Required]
        public string ProductPlatformLogo { get; set; }

        public List<Product> Products { get; set; }

        public int CountProducts => Products != null ? Products.Count : 0;
    }
}