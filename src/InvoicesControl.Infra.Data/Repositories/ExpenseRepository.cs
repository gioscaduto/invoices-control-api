using InvoicesControl.Application.Interfaces.Repositories;
using InvoicesControl.Domain.Entities;
using InvoicesControl.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace InvoicesControl.Infra.Data.Repositories
{
    public class ExpenseRepository : Repository<Expense>, IExpenseRepository
    {
        public ExpenseRepository(ApplicationDbContext db) : base(db)
        {
        }

        public new async Task<Expense> GetById(Guid id)
        {
            return await
            Db.Expenses
                .Include(e => e.Category)
                .Include(e => e.Customer)
                .FirstOrDefaultAsync(e => e.Id == id && !e.Deleted);
        }
    }
}
