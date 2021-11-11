using InvoicesControl.Application.ViewModels;
using System;
using System.Threading.Tasks;

namespace InvoicesControl.Application.Interfaces.Services
{
    public interface IExpenseCategoryService : IDisposable
    {
        public Task<Guid?> Add(ExpenseCategoryVm expenseCategoryVm);
        public Task<bool> Update(ExpenseCategoryEditVm expenseCategoryVm);
        Task<ExpenseCategoryDetailsVm> Get(Guid id);
        Task<ExpenseCategoryListVm> List(string name);
        public Task<bool> Activate(Guid id);
        public Task<bool> Inactivate(Guid id);
    }
}
