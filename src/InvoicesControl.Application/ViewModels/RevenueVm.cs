using InvoicesControl.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace InvoicesControl.Application.ViewModels
{
    public class RevenueVm
    {
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [Range(1, 999_999_999, ErrorMessage = "The field must be greater than 0 and less than or equal 999,999,999")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(255, ErrorMessage = "The field {0} must has between {2} and {1} characters", MinimumLength = 2)]
        public string InvoiceId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(1000, ErrorMessage = "The field {0} must has between {2} and {1} characters", MinimumLength = 2)]
        public string Description { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        public DateTime AccrualDate { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        public DateTime TransactionDate { get; set; }
              
        public Revenue ToRevenue(Guid customerId)
        {
            return new Revenue(Amount, InvoiceId, Description, AccrualDate, TransactionDate, customerId);
        }
    }

    public class RevenueEditVm
    {
        [Key]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [Range(1, 999_999_999, ErrorMessage = "The field must be greater than 0 and less than or equal 999,999,999")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(255, ErrorMessage = "The field {0} must has between {2} and {1} characters", MinimumLength = 2)]
        public string InvoiceId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(1000, ErrorMessage = "The field {0} must has between {2} and {1} characters", MinimumLength = 2)]
        public string Description { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        public DateTime AccrualDate { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        public DateTime TransactionDate { get; set; }

        public void UpdateRevenue(Revenue revenue)
        {
            revenue.Update(Amount, InvoiceId, Description, AccrualDate, TransactionDate);
        }
    }

    public class RevenueDetailsVm
    {
        [Key]        
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string InvoiceId { get; set; }
        public string Description { get; set; }
        public DateTime AccrualDate { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Customer { get; set; }

        public RevenueDetailsVm(Revenue revenue)
        {
            Id = revenue.Id;
            Amount = revenue.Amount;
            InvoiceId = revenue.InvoiceId;
            Description = revenue.Description;
            AccrualDate = revenue.AccrualDate;
            TransactionDate = revenue.TransactionDate;
            Customer = revenue.Customer.CommercialName;
        }
    }
}
