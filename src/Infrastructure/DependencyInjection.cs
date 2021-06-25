using System;
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
            string sql_server = Environment.GetEnvironmentVariable("SQL_SERVER");
            string db_name = Environment.GetEnvironmentVariable("DB_NAME");
            string sql_user = Environment.GetEnvironmentVariable("SQL_USER");
            string sql_password = Environment.GetEnvironmentVariable("SQL_PASSWORD");
            services.AddDbContext<CopperConsumptionDbContext>(options =>
                options.UseSqlServer(
                    connString.Replace("{SQL_SERVER}",sql_server)
                              .Replace("{DB_NAME}",db_name)
                              .Replace("{SQL_USER}",sql_user)
                              .Replace("{SQL_PASSWORD}",sql_password),
                    b => b.MigrationsAssembly(typeof(CopperConsumptionDbContext).Assembly.FullName)));

            services.AddScoped<ICopperConsumptionDbContext>(provider => provider.GetService<CopperConsumptionDbContext>());

            return services;
        }
    }
}