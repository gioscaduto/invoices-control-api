using InvoicesControl.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace InvoicesControl.Application.ViewModels.Users
{
    public class UserEditVm
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(255, ErrorMessage = "The field {0} must has between {2} and {1} characters", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [EmailAddress(ErrorMessage = "The field {0} has an invalid format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(100, ErrorMessage = "The field {0} must has between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirm password are diferents")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(100, ErrorMessage = "The field {0} must has between {2} and {1} characters", MinimumLength = 4)]
        public string Document { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(255, ErrorMessage = "The field {0} must has between {2} and {1} characters", MinimumLength = 1)]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(30, ErrorMessage = "The field {0} must has between {2} and {1} characters", MinimumLength = 8)]
        public string PhoneNumber { get; set; }

        public void UpdatePerson(Person person)
        {
            person.Update(Name, Document, CompanyName, PhoneNumber, Email);
        }
    }
}
