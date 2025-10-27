using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonDirectory.Domain.Entities;

namespace PersonDirectory.Infrastructure.Data.Configuration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Gender)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(p => p.PersonalNumber)
                .IsRequired()
                .HasMaxLength(11);

            builder.HasIndex(p => p.PersonalNumber)
                .IsUnique();

            builder.Property(p => p.BirthDate)
                .IsRequired();

            builder.Property(p => p.ImageUrl)
                .HasMaxLength(500);

            builder.HasOne(p => p.City)
                .WithOne()
                .HasForeignKey<Person>(p => p.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.PhoneNumbers)
                .WithOne(pn => pn.Person)
                .HasForeignKey(pn => pn.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Relations)
                .WithOne(r => r.Person)
                .HasForeignKey(r => r.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
