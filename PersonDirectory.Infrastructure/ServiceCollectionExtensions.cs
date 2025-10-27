using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonDirectory.Infrastructure.Data;

namespace PersonDirectory.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PersonDirectoryDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("PersonDirectoryConnection")));

            // Add other infrastructure services here
            return services;
        }
    }
}