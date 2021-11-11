using InvoicesControl.Application.Interfaces.Repositories;
using InvoicesControl.Domain.Entities;
using InvoicesControl.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoicesControl.Infra.Data.Repositories
{
    public class RevenueRepository : Repository<Revenue>, IRevenueRepository
    {
        public RevenueRepository(ApplicationDbContext db) : base(db)
        {
        }

        public new async Task<Revenue> GetById(Guid id)
        {
            return await
            Db.Revenues
                .Include(e => e.Customer)
                .FirstOrDefaultAsync(e => e.Id == id && !e.Deleted);
        }

        public async Task<decimal> TotalRevenueByYear(int fiscalYear)
        {
            return 
                await Db.Revenues
                .Include(r => r.Customer)
                .Where(r => !r.Deleted && !r.Customer.Deleted
                    && r.AccrualDate.Year == fiscalYear)
                .SumAsync(r => r.Amount);
        }

        public async Task<decimal> TotalRevenueByMonth(int fiscalMonth, int fiscalYear)
        {
            return
               await Db.Revenues
               .Include(r => r.Customer)
               .Where(r => !r.Deleted && !r.Customer.Deleted
                   && r.AccrualDate.Year == fiscalYear
                   && r.AccrualDate.Month == fiscalMonth)
               .SumAsync(r => r.Amount);
        }

        public async Task<IEnumerable<Revenue>> ListWithCustomer(int fiscalYear)
        {
            return
                await Db.Revenues
                .Include(r => r.Customer)
                .Where(r => !r.Deleted && !r.Customer.Deleted && r.Customer.Active
                    && r.AccrualDate.Year == fiscalYear).ToListAsync();
        }
    }
}
