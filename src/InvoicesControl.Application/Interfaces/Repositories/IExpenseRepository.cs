using InvoicesControl.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace InvoicesControl.Application.Interfaces.Repositories
{
    public interface IExpenseRepository : IRepository<Expense>
    {
        public new Task<Expense> GetById(Guid id);
    }
}
