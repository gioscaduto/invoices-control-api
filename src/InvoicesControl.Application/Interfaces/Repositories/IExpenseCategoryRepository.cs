using InvoicesControl.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace InvoicesControl.Application.Interfaces.Repositories
{
    public interface IExpenseCategoryRepository : IRepository<ExpenseCategory>
    {
        public Task<bool?> IsActive(Guid id);
        public Task<bool?> IsInactive(Guid id);
    }
}
