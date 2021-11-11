namespace InvoicesControl.Application.ViewModels.Reports
{
    public class TotalRevenueByYearReportVm
    {
        public TotalRevenueByYearReportVm(decimal totalRevenue, decimal maxRevenueAmount)
        {
            TotalRevenue = totalRevenue;
            MaxRevenueAmount = maxRevenueAmount;
        }

        public decimal TotalRevenue { get; private set; }
        public decimal MaxRevenueAmount { get; private set; }
    }
}
