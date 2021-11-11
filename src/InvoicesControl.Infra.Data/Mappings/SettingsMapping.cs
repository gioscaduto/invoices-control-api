using InvoicesControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoicesControl.Infra.Data.Mappings
{
    public class SettingsMapping : IEntityTypeConfiguration<Settings>
    {
        public void Configure(EntityTypeBuilder<Settings> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.MaxRevenueAmount)
               .IsRequired()
               .HasColumnType("decimal(18, 2)");

            builder.ToTable("Settings");
        }
    }
}
