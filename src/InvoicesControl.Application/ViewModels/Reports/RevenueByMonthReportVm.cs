using System.Collections.Generic;

namespace InvoicesControl.Application.ViewModels.Reports
{
    public class RevenueByMonthListReportVm
    {
        public RevenueByMonthListReportVm(IEnumerable<RevenueByMonthReportVm> revenues, decimal maxRevenueAmount)
        {
            Revenues = revenues;
            MaxRevenueAmount = maxRevenueAmount;
        }
        public IEnumerable<RevenueByMonthReportVm> Revenues { get; private set; }
        public decimal MaxRevenueAmount { get; private set; }
    }

    public class RevenueByMonthReportVm
    {
        public RevenueByMonthReportVm(string monthName, decimal totalRevenue)
        {
            MonthName = monthName;
            TotalRevenue = totalRevenue;
        }

        public string MonthName { get; private set; }
        public decimal TotalRevenue { get; private set; }
    }
}
