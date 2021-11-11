using InvoicesControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace InvoicesControl.Application.ViewModels
{
    public class ExpenseCategoryListVm
    {
        public ExpenseCategoryListVm(IEnumerable<ExpenseCategory> categories)
        {
            Categories = categories?.Select(c => new ExpenseCategoryDetailsVm(c));
            Count = categories?.Count() ?? 0;
        }

        public int Count { get; set; }
        public IEnumerable<ExpenseCategoryDetailsVm> Categories { get; set; }
    }

    public class ExpenseCategoryVm
    {
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(255, ErrorMessage = "The field {0} must has between {2} and {1} characters", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(1000, ErrorMessage = "The field {0} must has between {2} and {1} characters", MinimumLength = 2)]
        public string Description { get; set; }

        public ExpenseCategory ToExpenseCategory()
        {
            return new ExpenseCategory(Name, Description);
        }
    }

    public class ExpenseCategoryEditVm
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(255, ErrorMessage = "The field {0} must has between {2} and {1} characters", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(1000, ErrorMessage = "The field {0} must has between {2} and {1} characters", MinimumLength = 2)]
        public string Description { get; set; }

        public void UpdateExpenseCategory(ExpenseCategory expenseCategory)
        {
            expenseCategory.Update(Name, Description);
        }
    }

    public class ExpenseCategoryDetailsVm
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

        public ExpenseCategoryDetailsVm(ExpenseCategory expenseCategory)
        {
            Id = expenseCategory.Id;
            Name = expenseCategory.Name;
            Description = expenseCategory.Description;
            Active = expenseCategory.Active;
        }
    }
}
