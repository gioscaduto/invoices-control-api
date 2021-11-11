using System;

namespace InvoicesControl.Domain.Entities
{
    public class Expense : Entity
    {
        public Expense(decimal amount, string description, DateTime accrualDate, DateTime transactionDate, 
            Guid categoryId, Guid? customerId)
        {
            Amount = amount;
            Description = description;
            AccrualDate = accrualDate;
            TransactionDate = transactionDate;
            CategoryId = categoryId;
            CustomerId = customerId;
        }

        protected Expense() { }

        public decimal Amount { get; private set; }
        public string Description { get; private set; }
        public DateTime AccrualDate { get; private set; }
        public DateTime TransactionDate { get; private set; }
        public Guid CategoryId { get; private set; }
        public Guid? CustomerId { get; private set; }

        public ExpenseCategory Category { get; private set; }
        public Customer Customer { get; private set; }

        public void Update(decimal amount, string description, DateTime accrualDate, DateTime transactionDate, Guid? customerId)
        {
            Amount = amount;
            Description = description;
            AccrualDate = accrualDate;
            TransactionDate = transactionDate;
            CustomerId = customerId;
        }
    }
}
