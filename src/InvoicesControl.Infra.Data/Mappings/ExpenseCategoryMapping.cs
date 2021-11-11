using InvoicesControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoicesControl.Infra.Data.Mappings
{
    public class ExpenseCategoryMapping : IEntityTypeConfiguration<ExpenseCategory>
    {
        public void Configure(EntityTypeBuilder<ExpenseCategory> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(c => c.Description)
               .IsRequired()
               .HasColumnType("varchar(1000)");

            // 1 : N => ExpenseCategory : Expenses
            builder.HasMany(p => p.Expenses)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            builder.ToTable("ExpenseCategories");
        }
    }
}
