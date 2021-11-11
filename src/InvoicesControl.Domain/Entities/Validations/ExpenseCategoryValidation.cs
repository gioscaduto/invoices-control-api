using FluentValidation;

namespace InvoicesControl.Domain.Entities.Validations
{
    public class ExpenseCategoryValidation : AbstractValidator<ExpenseCategory>
    {
        public ExpenseCategoryValidation()
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

            RuleFor(c => c.Description)
               .NotEmpty()
                   .WithMessage("The field {PropertyName} is mandatory")
               .Length(2, 1000)
                   .WithMessage("The field {PropertyName} must has between {MinLength} and {MaxLength} characters");
        }
    }
}
