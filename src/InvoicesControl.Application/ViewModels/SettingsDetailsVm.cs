using InvoicesControl.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace InvoicesControl.Application.ViewModels
{
    public class SettingsEditVm
    {
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [Range(1, 999_999_999, ErrorMessage = "The field must be greater than 0 and less than or equal 999,999,999")]
        public decimal MaxRevenueAmount { get; set; }

        public void UpdateSettings(Settings settings)
        {
            settings.UpdateMaxRevenueAmount(MaxRevenueAmount);
        }
    }

    public class SettingsDetailsVm
    {
        public SettingsDetailsVm(Settings settings)
        {
            MaxRevenueAmount = settings.MaxRevenueAmount;
        }

        public decimal MaxRevenueAmount { get; private set; }
    }
}
