using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Parolă")]
        public string Password { get; set; }

        [Display(Name = "Ține-mă minte")]
        public bool RememberMe { get; set; }
    }
}
