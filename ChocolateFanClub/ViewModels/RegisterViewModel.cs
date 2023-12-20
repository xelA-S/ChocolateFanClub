using System.ComponentModel.DataAnnotations;

namespace ChocolateFanClub.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Email Address")]
        // if email is not provided, error message will be shown
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        // this compares the two passwords
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
