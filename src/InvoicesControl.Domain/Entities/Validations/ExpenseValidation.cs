using FluentValidation;

namespace InvoicesControl.Domain.Entities.Validations
{
    public class ExpenseValidation : AbstractValidator<Expense>
    {
        public ExpenseValidation()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .NotNull()
                    .WithMessage("The field {PropertyName} is mandatory");

            RuleFor(c => c.Amount)
                .NotNull()
                    .WithMessage("The field {PropertyName} is mandatory")
                .GreaterThan(0)
                    .WithMessage("The field must be greater than 0")
                .LessThanOrEqualTo(999_999_999)
                    .WithMessage("The field must be less than or equal 999,999,999");

            RuleFor(c => c.Description)
                .NotEmpty()
                    .WithMessage("The field {PropertyName} is mandatory")
                .Length(2, 1000)
                    .WithMessage("The field {PropertyName} must has between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.CategoryId)
               .NotEmpty()
               .NotNull()
                   .WithMessage("The field {PropertyName} is mandatory");

            RuleFor(c => c.AccrualDate)
               .NotNull()
                   .WithMessage("The field {PropertyName} is mandatory");

            RuleFor(c => c.TransactionDate)
               .NotNull()
                   .WithMessage("The field {PropertyName} is mandatory");
        }
    }
}
