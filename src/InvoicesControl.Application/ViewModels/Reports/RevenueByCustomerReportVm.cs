using System.Collections.Generic;

namespace InvoicesControl.Application.ViewModels.Reports
{
    public class RevenueByCustomerListReportVm
    {
        public RevenueByCustomerListReportVm(IEnumerable<RevenueByCustomerReportVm> revenues, decimal maxRevenueAmount)
        {
            Revenues = revenues;
            MaxRevenueAmount = maxRevenueAmount;
        }

        public IEnumerable<RevenueByCustomerReportVm> Revenues { get; private set; }
        public decimal MaxRevenueAmount { get; private set; }
    }

    public class RevenueByCustomerReportVm
    {
        public RevenueByCustomerReportVm(string customerName, decimal totalRevenue)
        {
            CustomerName = customerName;
            TotalRevenue = totalRevenue;
        }

        public string CustomerName { get; private set; }
        public decimal TotalRevenue { get; private set; }
    }
}
