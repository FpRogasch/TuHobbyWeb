using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TuHobbyWeb.Models.Entities
{
    public class Sale : BaseEntity
    {
        [Key]
        public int SaleId { get; set; }
        [Required]
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public User User { get; set; }
        [Required]
        public int SubTotal { get; set; }

        public int Tax { get; set; } = 19;

        public int Total => SubTotal * (Tax / 100);

        public DateTime? ConfirmedAt { get; set; }

        public List<SaleLine> Lines { get; set; }

    }
}