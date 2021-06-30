using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TuHobbyWeb.Models.Entities
{
    public class User : BaseEntity
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [MaxLength(60)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(60)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(120)]
        public string EmailAddress { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        //
        public byte[] PasswordSalt { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public DateTime? VerifiedAt { get; set; }
        [Required]
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string UserToken { get; set; }
    }
}