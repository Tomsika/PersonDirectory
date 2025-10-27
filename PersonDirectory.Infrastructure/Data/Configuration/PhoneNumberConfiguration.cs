using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonDirectory.Domain.Entities;

namespace PersonDirectory.Infrastructure.Data.Configuration
{
    public class PhoneNumberConfiguration : IEntityTypeConfiguration<PhoneNumber>
    {
        public void Configure(EntityTypeBuilder<PhoneNumber> builder)
        {
            builder.ToTable("PhoneNumbers");

            builder.HasKey(pn => pn.Id);

            builder.Property(pn => pn.Type)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(pn => pn.Number)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(pn => pn.PersonId)
                .IsRequired();
        }
    }
}