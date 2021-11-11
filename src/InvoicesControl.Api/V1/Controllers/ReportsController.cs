using InvoicesControl.Api.Controllers;
using InvoicesControl.Application.Interfaces;
using InvoicesControl.Application.Interfaces.Services;
using InvoicesControl.Application.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InvoicesControl.Api.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/reports")]
    public class ReportsController : MainController
    {
        private readonly IReportService _reportService;

        public ReportsController(INotifier notifier, IUser appUser, IReportService reportService) 
            : base(notifier, appUser)
        {
            _reportService = reportService;
        }

        [HttpGet("total-revenue/{fiscalYear:int:range(1900, 9999)}")]
        public async Task<ActionResult> TotalRevenue(int fiscalYear)
        {
            var totalRevenueByYearReport = await _reportService.TotalRevenueByYear(fiscalYear);

            return CustomResponse(totalRevenueByYearReport);
        }

        [HttpGet("total-revenue-by-month/{fiscalYear:int:range(1900, 9999)}")]
        public async Task<ActionResult> RevenueByMonth(int fiscalYear)
        {
            var totalRevenueByMonthReport = await _reportService.TotalRevenueByMonth(fiscalYear);

            return CustomResponse(totalRevenueByMonthReport);
        }

        [HttpGet("total-revenue-by-customer/{fiscalYear:int:range(1900, 9999)}")]
        public async Task<ActionResult> RevenueByCustomer(int fiscalYear)
        {
            var totalRevenueByCustomerReport = await _reportService.TotalRevenueByCustomer(fiscalYear);

            return CustomResponse(totalRevenueByCustomerReport);
        }
    }
}
