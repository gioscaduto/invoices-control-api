using InvoicesControl.Application.Interfaces.Repositories;
using InvoicesControl.Application.Interfaces.Services;
using InvoicesControl.Application.Notifications;
using InvoicesControl.Application.Validations;
using InvoicesControl.Application.ViewModels.Reports;
using InvoicesControl.Domain.Entities;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace InvoicesControl.Application.Services
{
    public class ReportService : BaseService, IReportService
    {
        private readonly IRevenueRepository _revenueRepository;
        private readonly ISettingsRepository _settingsRepository;

        public ReportService(INotifier notifier, IRevenueRepository revenueRepository,
            ISettingsRepository settingsRepository) : base(notifier)
        {
            _revenueRepository = revenueRepository;
            _settingsRepository = settingsRepository;
        }

        public async Task<TotalRevenueByYearReportVm> TotalRevenueByYear(int fiscalYear)
        {
            if (!ExecuteValidation(new YearValidation(), fiscalYear)) return null;

            var maxRevenueAmount = await GetMaxRevenueAmount();
            var totalRevenueByYear = await _revenueRepository.TotalRevenueByYear(fiscalYear);

            return new TotalRevenueByYearReportVm(totalRevenueByYear, maxRevenueAmount);
        }

        public async Task<RevenueByMonthListReportVm> TotalRevenueByMonth(int fiscalYear)
        {
            if (!ExecuteValidation(new YearValidation(), fiscalYear)) return null;

            var revenuesByMonths = new List<RevenueByMonthReportVm>();
            string monthName;
            decimal monthRevenue;

            for (int month = 1; month <= 12; month++)
            {
                monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
                monthRevenue = await _revenueRepository.TotalRevenueByMonth(month, fiscalYear);

                revenuesByMonths.Add(new RevenueByMonthReportVm(monthName, monthRevenue));
            }

            var maxRevenueAmount = await GetMaxRevenueAmount();
            return new RevenueByMonthListReportVm(revenuesByMonths, maxRevenueAmount);
        }

        public async Task<RevenueByCustomerListReportVm> TotalRevenueByCustomer(int fiscalYear)
        {
            if (!ExecuteValidation(new YearValidation(), fiscalYear)) return null;

            var revenues = await _revenueRepository.ListWithCustomer(fiscalYear);
            var maxRevenueAmount = await GetMaxRevenueAmount();

            if (revenues == null || !revenues.Any())
            {
                return new RevenueByCustomerListReportVm(null, maxRevenueAmount);
            }

            var revenuesByCustomer =
            revenues
                .GroupBy(r => r.CustomerId)
                .Select(r =>
                    new RevenueByCustomerReportVm
                    (
                        customerName: r.FirstOrDefault().Customer.CommercialName,
                        totalRevenue: r.Sum(re => re.Amount)
                    ));

            return new RevenueByCustomerListReportVm(revenuesByCustomer, maxRevenueAmount);
        }

        private async Task<decimal> GetMaxRevenueAmount()
        {
            var settings = (await _settingsRepository.Search(s => !s.Deleted)).FirstOrDefault();
            return settings?.MaxRevenueAmount ?? 0;
        }

        public void Dispose()
        {
             _revenueRepository?.Dispose();
             _settingsRepository?.Dispose();
        }
    }
}
