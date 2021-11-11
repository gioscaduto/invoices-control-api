using System.Collections.Generic;

namespace InvoicesControl.Domain.Entities
{
    public class Customer : Entity
    {
        public Customer(string document, string commercialName, string legalName, bool active = true)
        {
            Document = document;
            CommercialName = commercialName;
            LegalName = legalName;
            Active = active;
        }

        protected Customer() { }

        public string Document { get; private set; }
        public string CommercialName { get; private set; }
        public string LegalName { get; private set; }
        public bool Active { get; private set; }

        public IReadOnlyList<Revenue> Revenues { get; private set; }
        public IReadOnlyList<Expense> Expenses { get; private set; }

        public void Update(string document, string commercialName, string legalName)
        {
            Document = document;
            CommercialName = commercialName;
            LegalName = legalName;
        }

        public void Activate()
        {
            Active = true;
        }

        public void Inactivate()
        {
            Active = false;
        }

        public bool IsInactiveOrDeleted => !Active || Deleted;
    }
}
