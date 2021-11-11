using InvoicesControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoicesControl.Infra.Data.Mappings
{
    public class RevenueMapping : IEntityTypeConfiguration<Revenue>
    {
        public void Configure(EntityTypeBuilder<Revenue> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Amount)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");

            builder.Property(c => c.InvoiceId)
               .IsRequired()
               .HasColumnType("varchar(255)");

            builder.Property(c => c.Description)
               .IsRequired()
               .HasColumnType("varchar(1000)");

            builder.ToTable("Revenues");
        }
    }
}
