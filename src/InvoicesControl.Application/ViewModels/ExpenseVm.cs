using InvoicesControl.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace InvoicesControl.Application.ViewModels
{
    public class ExpenseVm
    {
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [Range(1, 999_999_999, ErrorMessage = "The field must be greater than 0 and less than or equal 999,999,999")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(1000, ErrorMessage = "The field {0} must has between {2} and {1} characters", MinimumLength = 2)]
        public string Description { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        public DateTime AccrualDate { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        public DateTime TransactionDate { get; set; }

        public Guid? CustomerId { get; set; }

        public Expense ToExpense(Guid categoryId)
        {
            return new Expense(Amount, Description, AccrualDate, TransactionDate, categoryId, CustomerId);
        }
    }

    public class ExpenseEditVm
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [Range(1, 999_999_999, ErrorMessage = "The field must be greater than 0 and less than or equal 999,999,999")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(1000, ErrorMessage = "The field {0} must has between {2} and {1} characters", MinimumLength = 1)]
        public string Description { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        public DateTime AccrualDate { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        public DateTime TransactionDate { get; set; }

        public Guid? CustomerId { get; set; }

        public void UpdateExpense(Expense expense)
        {
            expense.Update(Amount, Description, AccrualDate, TransactionDate, CustomerId);
        }
    }

    public class ExpenseDetailsVm
    {
        [Key]
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public DateTime AccrualDate { get; set; }

        public DateTime TransactionDate { get; set; }

        public Guid CategoryId { get; set; }
        public string Category { get; set; }

        public Guid? CustomerId { get; set; }

        public string Customer { get; set; }

        public ExpenseDetailsVm(Expense expense)
        {
            Id = expense.Id;
            Amount = expense.Amount;
            Description = expense.Description;
            AccrualDate = expense.AccrualDate;
            TransactionDate = expense.TransactionDate;
            CategoryId = expense.CategoryId;
            Category = expense.Category.Name;
            CustomerId = expense.CustomerId;
            Customer = expense.Customer?.CommercialName;
        }
    }
}
