namespace InvoicesControl.Domain.Entities
{
    public class Settings : Entity
    {
        public decimal MaxRevenueAmount { get; private set; }

        protected Settings() {}

        public void UpdateMaxRevenueAmount(decimal maxRevenueAmount)
        {
            MaxRevenueAmount = maxRevenueAmount;
        }
    }
}
