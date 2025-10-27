using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonDirectory.Domain.Entities;

namespace PersonDirectory.Infrastructure.Data.Configuration
{
    public class PersonRelationConfiguration : IEntityTypeConfiguration<PersonRelation>
    {
        public void Configure(EntityTypeBuilder<PersonRelation> builder)
        {
            builder.ToTable("PersonRelations");

            builder.HasKey(pr => pr.Id);

            builder.Property(pr => pr.RelationType)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(pr => pr.PersonId)
                .IsRequired();

            builder.Property(pr => pr.RelatedPersonId)
                .IsRequired();

            builder.HasIndex(pr => new { pr.PersonId, pr.RelatedPersonId })
                .IsUnique();

            builder.HasOne(pr => pr.Person)
           .WithMany(p => p.Relations)
           .HasForeignKey(pr => pr.PersonId)
           .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pr => pr.RelatedPerson)
                .WithMany()
                .HasForeignKey(pr => pr.RelatedPersonId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}