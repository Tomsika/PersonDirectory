using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonDirectory.Domain.Entities;

namespace PersonDirectory.Infrastructure.Data.Configuration
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Cities");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(c => c.Name)
                .IsUnique();

            builder.HasData(
                new City { Id = 1, Name = "თბილისი" },
                new City { Id = 2, Name = "ბათუმი" },
                new City { Id = 3, Name = "ქუთაისი" },
                new City { Id = 4, Name = "რუსთავი" },
                new City { Id = 5, Name = "გორი" },
                new City { Id = 6, Name = "ზუგდიდი" },
                new City { Id = 7, Name = "ფოთი" },
                new City { Id = 8, Name = "თელავი" }
            );
        }
    }
}