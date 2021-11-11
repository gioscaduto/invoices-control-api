using FluentValidation;

namespace InvoicesControl.Application.Validations
{
    public class YearValidation : AbstractValidator<int>
    {
        public YearValidation()
        {
            RuleFor(c => c)
                .NotEmpty()
                .NotNull()
                    .WithMessage("The field fiscal year is mandatory")
                .GreaterThanOrEqualTo(1900)
                .LessThanOrEqualTo(9999)
                    .WithMessage("The field fiscal year must be between 1900 and 9999.");
        }
    }
}
