using InvoicesControl.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace InvoicesControl.Application.Interfaces.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        public Task<bool?> IsActive(Guid id);
        public Task<bool?> IsInactive(Guid id);
    }
}
