using System.ComponentModel.DataAnnotations;

namespace ChocolateFanClub.ViewModels
{
    public class LoginViewModel
    {
        // Display represents what will be shown on the view
        [Display(Name = "Email Address")]
        // if email is not provided, error message will be shown
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
