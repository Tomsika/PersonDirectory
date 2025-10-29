using Microsoft.EntityFrameworkCore;
using PersonDirectory.Domain.Entities;

namespace PersonDirectory.Infrastructure.Data
{
    public class PersonDirectoryDbContext : DbContext
    {
        public PersonDirectoryDbContext(DbContextOptions<PersonDirectoryDbContext> options)
        : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        public DbSet<PersonRelation> PersonRelations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonDirectoryDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}