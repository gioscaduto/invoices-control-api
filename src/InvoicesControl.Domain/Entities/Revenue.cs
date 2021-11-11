using System;

namespace InvoicesControl.Domain.Entities
{
    public class Revenue : Entity //Receita
    {
        public Revenue(decimal amount, string invoiceId, string description, DateTime accrualDate, 
            DateTime transactionDate, Guid customerId)
        {
            Amount = amount;
            InvoiceId = invoiceId;
            Description = description;
            AccrualDate = accrualDate;
            TransactionDate = transactionDate;
            CustomerId = customerId;
        }

        protected Revenue() { }

        public decimal Amount { get; private set; }
        public string InvoiceId { get; private set; }
        public string Description { get; private set; }
        public DateTime AccrualDate { get; private set; }
        public DateTime TransactionDate { get; private set; }
        public Guid CustomerId { get; private set; }   
        
        public Customer Customer { get; private set; }

        public void Update(decimal amount, string invoiceId, string description, DateTime accrualDate,
           DateTime transactionDate)
        {
            Amount = amount;
            InvoiceId = invoiceId;
            Description = description;
            AccrualDate = accrualDate;
            TransactionDate = transactionDate;
        }
    }
}
