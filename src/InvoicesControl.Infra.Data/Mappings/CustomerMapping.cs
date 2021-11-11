using InvoicesControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoicesControl.Infra.Data.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Document)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.CommercialName)
               .IsRequired()
               .HasColumnType("varchar(255)");

            builder.Property(c => c.LegalName)
               .IsRequired()
               .HasColumnType("varchar(255)");

            // 1 : N => Expense : Expenses
            builder.HasMany(p => p.Expenses)
                .WithOne(p => p.Customer)
                .IsRequired(false)
                .HasForeignKey(p => p.CustomerId);

            // 1 : N => ExpenseCategory : Revenues
            builder.HasMany(p => p.Revenues)
                .WithOne(p => p.Customer)
                .HasForeignKey(p => p.CustomerId);

            builder.ToTable("Customers");
        }
    }
}