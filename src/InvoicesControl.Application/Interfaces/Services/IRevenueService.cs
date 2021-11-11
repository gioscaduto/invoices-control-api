using InvoicesControl.Application.ViewModels;
using System;
using System.Threading.Tasks;

namespace InvoicesControl.Application.Interfaces.Services
{
    public interface IRevenueService : IDisposable
    {
        public Task<Guid?> Add(Guid customerId, RevenueVm revenueVm);
        public Task<bool> Update(RevenueEditVm revenueVm);
        public Task<bool> Delete(Guid id);
        public Task<RevenueDetailsVm> Get(Guid id);
    }
}
