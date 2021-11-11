using System.ComponentModel.DataAnnotations;

namespace InvoicesControl.Application.ViewModels.Users
{
    public class LoginUserVm
    {
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [EmailAddress(ErrorMessage = "The field {0} has an invalid format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(100, ErrorMessage = "The field {0} must has between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
