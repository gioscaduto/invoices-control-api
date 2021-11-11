using FluentValidation;

namespace InvoicesControl.Domain.Entities.Validations
{
    public class CustomerValidation : AbstractValidator<Customer>
    {
        public CustomerValidation()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .NotNull()
                    .WithMessage("The field {PropertyName} is mandatory");

            RuleFor(c => c.Document)
              .NotEmpty()
                  .WithMessage("The field {PropertyName} is mandatory")
              .Length(4, 100)
                  .WithMessage("The field {PropertyName} must has between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.CommercialName)
              .NotEmpty()
                  .WithMessage("The field {PropertyName} is mandatory")
              .Length(2, 255)
                  .WithMessage("The field {PropertyName} must has between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.LegalName)
              .NotEmpty()
                  .WithMessage("The field {PropertyName} is mandatory")
              .Length(2, 255)
                  .WithMessage("The field {PropertyName} must has between {MinLength} and {MaxLength} characters");
        }
    }
}