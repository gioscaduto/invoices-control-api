using System.Collections.Generic;

namespace InvoicesControl.Domain.Entities
{
    public class ExpenseCategory : Entity
    {
        public ExpenseCategory(string name, string description, bool active = true)
        {
            Name = name;
            Description = description;
            Active = active;
        }

        protected ExpenseCategory() { }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Active { get; private set; }
        public IReadOnlyList<Expense> Expenses { get; private set; }

        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
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
