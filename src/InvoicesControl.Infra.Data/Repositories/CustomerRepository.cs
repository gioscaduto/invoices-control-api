using InvoicesControl.Application.Interfaces.Repositories;
using InvoicesControl.Domain.Entities;
using InvoicesControl.Infra.Data.Contexts;
using System;
using System.Threading.Tasks;

namespace InvoicesControl.Infra.Data.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<bool?> IsActive(Guid id)
        {
            var customer = await GetById(id);
            if (customer == null) return null;

            return customer.Active;
        }

        public async Task<bool?> IsInactive(Guid id)
        {
            var customer = await GetById(id);
            if (customer == null) return null;

            return !customer.Active;
        }
    }
}
