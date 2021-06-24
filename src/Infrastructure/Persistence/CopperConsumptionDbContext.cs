using CopperConsumption.Application.Common.Interfaces;
using CopperConsumption.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;

namespace CopperConsumption.Infrastructure.Persistence
{
    public class CopperConsumptionDbContext : DbContext, ICopperConsumptionDbContext
    {
        public CopperConsumptionDbContext(
            DbContextOptions options
            ) : base(options)
        { }

        public DbSet<Pais> Paises { get; set; }

        public DbSet<Consumo> Consumos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
