using InvoicesControl.Application.ViewModels;
using System;
using System.Threading.Tasks;

namespace InvoicesControl.Application.Interfaces.Services
{
    public interface ICustomerService : IDisposable
    {
        public Task<Guid?> Add(CustomerVm customer);
        public Task<bool> Update(CustomerEditVm customer);
        public Task<bool> Activate(Guid id);
        public Task<bool> Inactivate(Guid id);
        public Task<CustomerDetailVm> Get(Guid id);
        public Task<CustomerListVm> List(string legalName, string document);
    }
}
