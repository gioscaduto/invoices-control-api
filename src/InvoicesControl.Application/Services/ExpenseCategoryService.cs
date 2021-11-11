using InvoicesControl.Application.Interfaces.Repositories;
using InvoicesControl.Application.Interfaces.Services;
using InvoicesControl.Application.Notifications;
using InvoicesControl.Application.ViewModels;
using InvoicesControl.Domain.Entities.Validations;
using System;
using System.Threading.Tasks;

namespace InvoicesControl.Application.Services
{
    public class ExpenseCategoryService : BaseService, IExpenseCategoryService
    {
        private const string EXPENSE_CATEGORY_NOT_FOUND_MESSAGE = "Expense category not found";

        private readonly IExpenseCategoryRepository _expenseCategoryRepository;

        public ExpenseCategoryService(INotifier notifier, IExpenseCategoryRepository expenseCategoryRepository)
            : base(notifier)
        {
            _expenseCategoryRepository = expenseCategoryRepository;
        }

        public async Task<Guid?> Add(ExpenseCategoryVm expenseCategoryVm)
        {
            var expenseCategory = expenseCategoryVm.ToExpenseCategory();

            if (!ExecuteValidation(new ExpenseCategoryValidation(), expenseCategory)) return null;

            await _expenseCategoryRepository.Add(expenseCategory);

            return expenseCategory.Id;
        }

        public async Task<bool> Update(ExpenseCategoryEditVm expenseCategoryVm)
        {
            var expenseCategory = await _expenseCategoryRepository.GetById(expenseCategoryVm.Id);

            if (expenseCategory == null)
            {
                Notify(EXPENSE_CATEGORY_NOT_FOUND_MESSAGE);
                return false;
            }

            expenseCategoryVm.UpdateExpenseCategory(expenseCategory);

            if (!ExecuteValidation(new ExpenseCategoryValidation(), expenseCategory)) return false;

            await _expenseCategoryRepository.Update(expenseCategory);

            return true;
        }

        public async Task<ExpenseCategoryDetailsVm> Get(Guid id)
        {
            var expenseCategory = await _expenseCategoryRepository.GetById(id);

            if (expenseCategory == null)
            {
                Notify(EXPENSE_CATEGORY_NOT_FOUND_MESSAGE);
                return null;
            }

            return new ExpenseCategoryDetailsVm(expenseCategory);
        }

        public async Task<ExpenseCategoryListVm> List(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                name = name.Trim().ToLower();
            }

            var expenseCategories = await _expenseCategoryRepository.Search
                (c => string.IsNullOrEmpty(name) || c.Name.ToLower().Contains(name));


            return new ExpenseCategoryListVm(expenseCategories);
        }

        public async Task<bool> Activate(Guid id)
        {
            var expenseCategory = await _expenseCategoryRepository.GetById(id);

            if (expenseCategory == null)
            {
                Notify(EXPENSE_CATEGORY_NOT_FOUND_MESSAGE);
                return false;
            }

            expenseCategory.Activate();
            await _expenseCategoryRepository.Update(expenseCategory);

            return true;
        }

        public async Task<bool> Inactivate(Guid id)
        {
            var expenseCategory = await _expenseCategoryRepository.GetById(id);

            if (expenseCategory == null)
            {
                Notify(EXPENSE_CATEGORY_NOT_FOUND_MESSAGE);
                return false;
            }

            expenseCategory.Inactivate();
            await _expenseCategoryRepository.Update(expenseCategory);

            return true;
        }

        public void Dispose()
        {
            _expenseCategoryRepository?.Dispose();
        }
    }
}
