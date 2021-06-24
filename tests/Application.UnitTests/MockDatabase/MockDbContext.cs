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

        // public static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        // {
        //     var queryable = sourceList.AsQueryable();
        //     var dbSet = new Mock<DbSet<T>>();
        //     dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        //     dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        //     dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        //     dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
        //     dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));
        //     return dbSet.Object;
        // }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var task = new TaskCompletionSource<int>();
            task.SetResult(1);
            return task.Task;
        }
    }
}