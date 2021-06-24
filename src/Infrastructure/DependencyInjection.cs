using CopperConsumption.Application.Common.Interfaces;
using CopperConsumption.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CopperConsumption.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string connString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<CopperConsumptionDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(CopperConsumptionDbContext).Assembly.FullName)));

            services.AddScoped<ICopperConsumptionDbContext>(provider => provider.GetService<CopperConsumptionDbContext>());

            return services;
        }
    }
}