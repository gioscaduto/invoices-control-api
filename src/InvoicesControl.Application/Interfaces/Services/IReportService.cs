using InvoicesControl.Application.ViewModels.Reports;
using System;
using System.Threading.Tasks;

namespace InvoicesControl.Application.Interfaces.Services
{
    public interface IReportService : IDisposable
    {
        Task<TotalRevenueByYearReportVm> TotalRevenueByYear(int fiscalYear);
        Task<RevenueByMonthListReportVm> TotalRevenueByMonth(int fiscalYear);
        Task<RevenueByCustomerListReportVm> TotalRevenueByCustomer(int fiscalYear);
    }
}
