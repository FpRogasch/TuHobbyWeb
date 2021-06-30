using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TuHobbyWeb.Models.ViewModels
{
    public class ProductCreateViewModel
    {
        public int? ProductId { get; set; }
        [Required]
        public string ProductCode { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int? ProductPrice { get; set; }
        [Required]
        public int? ProductStock { get; set; }
    }
}