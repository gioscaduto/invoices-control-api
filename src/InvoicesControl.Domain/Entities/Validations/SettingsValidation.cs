using FluentValidation;

namespace InvoicesControl.Domain.Entities.Validations
{
    public class SettingsValidation : AbstractValidator<Settings>
    {
        public SettingsValidation()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .NotNull()
                    .WithMessage("The field {PropertyName} is mandatory");

            RuleFor(c => c.MaxRevenueAmount)
                .NotNull()
                    .WithMessage("The field {PropertyName} is mandatory")
                .GreaterThan(0)
                    .WithMessage("The field must be greater than 0")
                .LessThanOrEqualTo(999_999_999)
                    .WithMessage("The field must be less than or equal 999,999,999");
        }
    }
}
