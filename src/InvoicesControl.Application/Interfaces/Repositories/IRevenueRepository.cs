using InvoicesControl.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoicesControl.Application.Interfaces.Repositories
{
    public interface IRevenueRepository : IRepository<Revenue>
    {
        public Task<decimal> TotalRevenueByYear(int fiscalYear);

        public Task<decimal> TotalRevenueByMonth(int month, int year);

        Task<IEnumerable<Revenue>> ListWithCustomer(int fiscalYear);
    }
}
