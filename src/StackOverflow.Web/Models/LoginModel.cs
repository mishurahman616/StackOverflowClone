using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Web.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Valid Email Address is required")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
