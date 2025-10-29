using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonDirectory.Domain.Interfaces;
using PersonDirectory.Infrastructure.Data;
using PersonDirectory.Infrastructure.Repositories;
using PersonDirectory.Infrastructure.UnitOfWorks;

namespace PersonDirectory.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PersonDirectoryDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("PersonDirectoryConnection")));

            services.AddScoped<IPersonWriteRepository, PersonWriteRepository>();
            services.AddScoped<IPersonReadRepository, PersonReadRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}