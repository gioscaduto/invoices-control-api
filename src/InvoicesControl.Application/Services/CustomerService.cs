using InvoicesControl.Application.Interfaces.Repositories;
using InvoicesControl.Application.Interfaces.Services;
using InvoicesControl.Application.Notifications;
using InvoicesControl.Application.ViewModels;
using InvoicesControl.Domain.Entities.Validations;
using System;
using System.Threading.Tasks;

namespace InvoicesControl.Application.Services
{
    public class CustomerService : BaseService, ICustomerService
    {
        private const string CUSTOMER_NOT_FOUND_MESSAGE = "Customer not found";

        private readonly ICustomerRepository _customerRepository;

        public CustomerService(INotifier notifier, ICustomerRepository customerRepository) : base(notifier)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Guid?> Add(CustomerVm customerVm)
        {
            var customer = customerVm.ToCustomer();

            if (!ExecuteValidation(new CustomerValidation(), customer)) return null;

            await _customerRepository.Add(customer);

            return customer.Id;
        }

        public async Task<bool> Update(CustomerEditVm customerVm)
        {
            var customer = await _customerRepository.GetById(customerVm.Id);

            if (customer == null)
            {
                Notify(CUSTOMER_NOT_FOUND_MESSAGE);
                return false;
            }

            customerVm.UpdateCustomer(customer);

            if (!ExecuteValidation(new CustomerValidation(), customer)) return false;

            await _customerRepository.Update(customer);

            return true;
        }

        public async Task<bool> Activate(Guid id)
        {
            var customer = await _customerRepository.GetById(id);

            if (customer == null)
            {
                Notify(CUSTOMER_NOT_FOUND_MESSAGE);
                return false;
            }

            customer.Activate();
            await _customerRepository.Update(customer);

            return true;
        }

        public async Task<bool> Inactivate(Guid id)
        {
            var customer = await _customerRepository.GetById(id);

            if (customer == null)
            {
                Notify(CUSTOMER_NOT_FOUND_MESSAGE);
                return false;
            }

            customer.Inactivate();
            await _customerRepository.Update(customer);

            return true;
        }

        public async Task<CustomerDetailVm> Get(Guid id)
        {
            var customer = await _customerRepository.GetById(id);

            if (customer == null)
            {
                Notify(CUSTOMER_NOT_FOUND_MESSAGE);
                return null;
            }

            return new CustomerDetailVm(customer);
        }

        public async Task<CustomerListVm> List(string legalName, string document)
        {
            if (!string.IsNullOrEmpty(legalName))
            {
                legalName = legalName.Trim().ToLower();
            }

            if (!string.IsNullOrEmpty(document))
            {
                document = document.Trim().ToLower();
            }

            var customers = await _customerRepository.Search
                (c => (string.IsNullOrEmpty(legalName) || c.LegalName.ToLower().Contains(legalName)) &&
                      (string.IsNullOrEmpty(document) || c.Document.ToLower().Contains(document)));

            return new CustomerListVm(customers);
        }

        public void Dispose()
        {
            _customerRepository?.Dispose();
        }
    }
}
