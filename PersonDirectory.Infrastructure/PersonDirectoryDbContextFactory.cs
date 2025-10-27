using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PersonDirectory.Infrastructure.Data;

namespace PersonDirectory.Infrastructure
{
    public class PersonDirectoryDbContextFactory : IDesignTimeDbContextFactory<PersonDirectoryDbContext>
    {
        public PersonDirectoryDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../PersonDirectory.API"))
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<PersonDirectoryDbContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("PersondirectoryConnection"));

            return new PersonDirectoryDbContext(optionsBuilder.Options);
        }
    }
}