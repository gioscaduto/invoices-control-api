using InvoicesControl.Application.Interfaces.Repositories;
using InvoicesControl.Application.Interfaces.Services;
using InvoicesControl.Application.Notifications;
using InvoicesControl.Application.ViewModels;
using InvoicesControl.Domain.Entities.Validations;
using System;
using System.Threading.Tasks;

namespace InvoicesControl.Application.Services
{
    public class ExpenseService : BaseService, IExpenseService
    {
        private const string EXPENSE_NOT_FOUND_MESSAGE = "Expense not found";

        private readonly IExpenseRepository _expenseRepository;
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;
        private readonly ICustomerRepository _customerRepository;

        public ExpenseService(INotifier notifier, IExpenseRepository expenseRepository,
            IExpenseCategoryRepository expenseCategoryRepository, ICustomerRepository customerRepository) : base(notifier)
        {
            _expenseRepository = expenseRepository;
            _expenseCategoryRepository = expenseCategoryRepository;
            _customerRepository = customerRepository;
        }

        public async Task<Guid?> Add(Guid categoryId, ExpenseVm expenseVm)
        {
            var category = await _expenseCategoryRepository.GetById(categoryId);

            if (category == null)
            {
                Notify("Category not found");
                return null;
            }

            if (category.IsInactiveOrDeleted)
            {
                Notify("This category is invalid");
                return null;
            }

            if (await CustomerNotFound(expenseVm.CustomerId)) return null;
            
            var expense = expenseVm.ToExpense(categoryId);

            if (!ExecuteValidation(new ExpenseValidation(), expense)) return null;

            await _expenseRepository.Add(expense);

            return expense.Id;
        }

        public async Task<bool> Delete(Guid id)
        {
            var expense = await _expenseRepository.GetById(id);

            if (expense == null)
            {
                Notify(EXPENSE_NOT_FOUND_MESSAGE);
                return false;
            }

            expense.Delete();
            await _expenseRepository.Update(expense);

            return true;
        }

        public async Task<bool> Update(ExpenseEditVm expenseVm)
        {
            var expense = await _expenseRepository.GetById(expenseVm.Id);

            if (expense == null)
            {
                Notify(EXPENSE_NOT_FOUND_MESSAGE);
                return false;
            }

            if (await CustomerNotFound(expenseVm.CustomerId)) return false;

            expenseVm.UpdateExpense(expense);

            if (!ExecuteValidation(new ExpenseValidation(), expense)) return false;

            await _expenseRepository.Update(expense);

            return true;
        }

        public async Task<ExpenseDetailsVm> Get(Guid id)
        {
            var expense = await _expenseRepository.GetById(id);

            if (expense == null)
            {
                Notify(EXPENSE_NOT_FOUND_MESSAGE);
                return null;
            }

            return new ExpenseDetailsVm(expense);
        }

        private async Task<bool> CustomerNotFound(Guid? customerId)
        {
            if (customerId.HasValue)
            {
                var customer = await _customerRepository.GetById(customerId.Value);

                if (customer == null)
                {
                    Notify("Customer not found");
                    return true;
                }
            }

            return false;
        }

        public void Dispose()
        {
            _expenseRepository.Dispose();
            _expenseCategoryRepository.Dispose();
        }
    }
}
