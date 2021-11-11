using InvoicesControl.Application.Interfaces.Repositories;
using InvoicesControl.Application.Interfaces.Services;
using InvoicesControl.Application.Notifications;
using InvoicesControl.Application.ViewModels;
using InvoicesControl.Domain.Entities.Validations;
using System;
using System.Threading.Tasks;

namespace InvoicesControl.Application.Services
{
    public class RevenueService : BaseService, IRevenueService
    {
        private const string REVENUE_NOT_FOUND_MESSAGE = "Revenue not found";

        private readonly IRevenueRepository _revenueRepository;
        private readonly ICustomerRepository _customerRepository;
        
        public RevenueService(INotifier notifier, IRevenueRepository revenueRepository,
            ICustomerRepository customerRepository) : base(notifier)
        {
            _revenueRepository = revenueRepository;
            _customerRepository = customerRepository;
        }

        public async Task<Guid?> Add(Guid customerId, RevenueVm revenueVm)
        {
            var customer = await _customerRepository.GetById(customerId);

            if (customer == null)
            {
                Notify("Customer not found");
                return null;
            }

            if (customer.IsInactiveOrDeleted)
            {
                Notify("This customer is invalid");
                return null;
            }

            var revenue = revenueVm.ToRevenue(customerId);

            if (!ExecuteValidation(new RevenueValidation(), revenue)) return null;

            await _revenueRepository.Add(revenue);

            return revenue.Id;
        }

        public async Task<bool> Delete(Guid id)
        {
            var revenue = await _revenueRepository.GetById(id);

            if (revenue == null)
            {
                Notify(REVENUE_NOT_FOUND_MESSAGE);
                return false;
            }

            revenue.Delete();
            await _revenueRepository.Update(revenue);

            return true;
        }

        public async Task<bool> Update(RevenueEditVm revenueVm)
        {
            var revenue = await _revenueRepository.GetById(revenueVm.Id);

            if (revenue == null)
            {
                Notify(REVENUE_NOT_FOUND_MESSAGE);
                return false;
            }

            revenueVm.UpdateRevenue(revenue);

            if (!ExecuteValidation(new RevenueValidation(), revenue)) return false;

            await _revenueRepository.Update(revenue);

            return true;
        }

        public async Task<RevenueDetailsVm> Get(Guid id)
        {
            var revenue = await _revenueRepository.GetById(id);

            if (revenue == null)
            {
                Notify(REVENUE_NOT_FOUND_MESSAGE);
                return null;
            }

            return new RevenueDetailsVm(revenue);
        }

        public void Dispose()
        {
            _revenueRepository?.Dispose();
        }
    }
}
