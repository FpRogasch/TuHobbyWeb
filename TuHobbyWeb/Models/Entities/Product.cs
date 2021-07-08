using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TuHobbyWeb.Models.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductCode { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required] 
        public int ProductPrice { get; set; }
        [Required]
        public int ProductStock { get; set; }

        [Required]
        public int PlatformId { get; set; }
        [ForeignKey("PlatformId")]
        public ProductPlatform ProductPlatform { get; set; }
        [Required]
        public string ProductImage { get; set; }
    }
}