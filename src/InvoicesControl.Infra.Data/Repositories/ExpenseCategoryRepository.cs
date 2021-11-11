using InvoicesControl.Application.Interfaces.Repositories;
using InvoicesControl.Domain.Entities;
using InvoicesControl.Infra.Data.Contexts;
using System;
using System.Threading.Tasks;

namespace InvoicesControl.Infra.Data.Repositories
{
    public class ExpenseCategoryRepository : Repository<ExpenseCategory>, IExpenseCategoryRepository
    {
        public ExpenseCategoryRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<bool?> IsActive(Guid id)
        {
            var expenseCategory = await GetById(id);
            if (expenseCategory == null) return null;

            return expenseCategory.Active;
        }

        public async Task<bool?> IsInactive(Guid id)
        {
            var expenseCategory = await GetById(id);
            if (expenseCategory == null) return null;

            return !expenseCategory.Active;
        }
    }
}
