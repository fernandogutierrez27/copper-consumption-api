using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CopperConsumption.Application.Common.Interfaces;
using CopperConsumption.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CopperConsumption.Application.UnitTests.MockDatabase
{
    public class MockDbContext : ICopperConsumptionDbContext
    {
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Consumo> Consumos { get; set; }

        public MockDbContext(DbSet<Pais> paises)
        {
            Paises = paises;
        }

        public MockDbContext(DbSet<Consumo> consumos)
        {
            Consumos = consumos;
        }

        public MockDbContext(DbSet<Pais> paises, DbSet<Consumo> consumos)
        {
            Paises = paises;
            Consumos = consumos;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var task = new TaskCompletionSource<int>();
            task.SetResult(1);
            return task.Task;
        }
    }
}