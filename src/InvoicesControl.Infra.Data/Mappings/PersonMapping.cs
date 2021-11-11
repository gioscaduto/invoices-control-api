using InvoicesControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoicesControl.Infra.Data.Mappings
{
    public class PersonMapping : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(c => c.Document)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.CompanyName)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(c => c.PhoneNumber)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.ToTable("Persons");
        }
    }
}
