using AutoMapper;
using CopperConsumption.Application.Consumos;
using CopperConsumption.Application.Paises;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CopperConsumption.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<PaisService>();
            services.AddScoped<ConsumoService>();

            return services;
        }
    }
}
