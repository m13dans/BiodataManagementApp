using System.ComponentModel.DataAnnotations;

namespace PT_EDI_Indonesia_MVC.Core.Models
{
    public class UserSignUpModel
    {
        [Required]
        public string? NameLengkap { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}