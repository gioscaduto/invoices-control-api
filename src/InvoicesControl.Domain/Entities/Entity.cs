using System;

namespace InvoicesControl.Domain.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public bool Deleted { get; protected set; }
        public DateTime? DeletedAt { get; protected set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public void Delete()
        {
            Deleted = true;
            DeletedAt = DateTime.UtcNow;
        }
    }
}
