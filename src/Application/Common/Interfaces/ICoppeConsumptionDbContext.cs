using System.Threading;
using System.Threading.Tasks;
using CopperConsumption.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CopperConsumption.Application.Common.Interfaces
{
    public interface ICopperConsumptionDbContext
    {
        DbSet<Pais> Paises { get; set; }
        DbSet<Consumo> Consumos { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
