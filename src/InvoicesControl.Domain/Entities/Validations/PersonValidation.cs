using FluentValidation;

namespace InvoicesControl.Domain.Entities.Validations
{
    public class PersonValidation : AbstractValidator<Person>
    {
        public PersonValidation()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .NotNull()
                    .WithMessage("The field {PropertyName} is mandatory");

            RuleFor(c => c.Name)
                .NotEmpty()
                    .WithMessage("The field {PropertyName} is mandatory")
                .Length(2, 255)
                    .WithMessage("The field {PropertyName} must has between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.Email)
                .NotEmpty()
                .NotNull()
                    .WithMessage("The field {PropertyName} is mandatory")
                .EmailAddress()
                    .WithMessage("This e-mail is invalid");

            RuleFor(c => c.Document)
                .NotEmpty()
                    .WithMessage("The field {PropertyName} is mandatory")
                .Length(4, 100)
                    .WithMessage("The field {PropertyName} must has between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.CompanyName)
                .NotEmpty()
                    .WithMessage("The field {PropertyName} is mandatory")
                .Length(1, 255)
                    .WithMessage("The field {PropertyName} must has between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.PhoneNumber)
                .NotEmpty()
                    .WithMessage("The field {PropertyName} is mandatory")
                .Length(8, 30)
                    .WithMessage("The field {PropertyName} must has between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.UserId)
               .NotEmpty()
               .NotNull()
                   .WithMessage("The field {PropertyName} is mandatory");
        }
    }
}
