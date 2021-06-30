using System.ComponentModel.DataAnnotations;

namespace TuHobbyWeb.Models.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required]
        [MaxLength(60)]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(60)]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(120)]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [Compare("Password")] // -> Compara ambas password, para ver si coinciden
        [Display(Name = "Confirmar Password")]
        public string PasswordConfirm { get; set; }
    }
}