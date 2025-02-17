using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Nume utilizator")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Adresă de email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Parolă")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Parolele nu se potrivesc.")]
        [Display(Name = "Confirmă parola")]
        public string ConfirmPassword { get; set; }
    }
}
