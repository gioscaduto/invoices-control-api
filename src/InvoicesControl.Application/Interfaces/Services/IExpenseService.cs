using InvoicesControl.Application.ViewModels;
using System;
using System.Threading.Tasks;

namespace InvoicesControl.Application.Interfaces.Services
{
    public interface IExpenseService : IDisposable
    {
        public Task<Guid?> Add(Guid categoryId, ExpenseVm expenseVm);
        public Task<bool> Update(ExpenseEditVm expenseVm);
        public Task<bool> Delete(Guid id);
        public Task<ExpenseDetailsVm> Get(Guid id);
    }
}
