using CopperConsumption.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CopperConsumption.Application.Common.Interfaces
{
    public interface ICopperConsumptionDbContext
    {
        DbSet<Pais> Paises { get; set; }
        DbSet<Consumo> Consumos { get; set; }
    }
}
