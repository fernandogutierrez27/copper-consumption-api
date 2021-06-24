using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CopperConsumption.Application.Common.Exceptions;
using CopperConsumption.Application.Common.Interfaces;
using CopperConsumption.Application.Common.Mappings;
using CopperConsumption.Application.Consumos;
using CopperConsumption.Application.Paises;
using CopperConsumption.Application.UnitTests.MockDatabase;
using CopperConsumption.Domain.Entities;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;

namespace CopperConsumption.Application.UnitTests.Tests
{
    [TestFixture]
    public class ConsumoServiceTests
    {
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _configuration;

        ConsumoService systemUnderTest;

        public ConsumoServiceTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [TestCase(1, 2021, 33.5)]
        public async Task CrearConsumo_Nuevo_RetornaExpected(int idPais, int año, decimal cantidad)
        {
            var paises = MockDbSeed.CreateListPaises();
            var mockPaises = paises.AsQueryable().BuildMockDbSet();

            var consumosOriginal = MockDbSeed.CreateListConsumos();
            var consumos = MockDbSeed.CreateListConsumos();
            var mockConsumos = consumos.AsQueryable().BuildMockDbSet();


            mockPaises.Setup(x => x.FindAsync(It.IsAny<object[]>())).ReturnsAsync((object[] ids) =>
            {
                var id = (int)ids.First();
                return paises.FirstOrDefault(x => x.Id == id);
            });

            mockConsumos.Setup(m => m.Add(It.IsAny<Consumo>())).Callback<Consumo>((entity) => consumos.Add(entity));

            var dbContext = new MockDbContext(mockPaises.Object, mockConsumos.Object);

            systemUnderTest = new ConsumoService(dbContext, _mapper);

            //act
            var result = await systemUnderTest.CreateConsumo(new ConsumoCommand { PaisId = idPais, Año = año, Cantidad = cantidad });

            // assert
            // Como no se genera un nuevo id, el resultado debe ser 0 (y no una excepción)
            Assert.AreEqual(consumosOriginal.Count() + 1, consumos.Count());
        }

        [TestCase(1, 2020, 33.5)]
        public void CrearConsumo_Existente_RetornaExcepcion(int idPais, int año, decimal cantidad)
        {
            //arrange
            var paises = MockDbSeed.CreateListPaises();
            var mockPaises = paises.AsQueryable().BuildMockDbSet();

            var consumos = MockDbSeed.CreateListConsumos();
            var mockConsumos = consumos.AsQueryable().BuildMockDbSet();


            mockPaises.Setup(x => x.FindAsync(It.IsAny<object[]>())).ReturnsAsync((object[] ids) =>
            {
                var id = (int)ids.First();
                return paises.FirstOrDefault(x => x.Id == id);
            });

            mockConsumos.Setup(m => m.Add(It.IsAny<Consumo>())).Callback<Consumo>((entity) => consumos.Add(entity));

            var dbContext = new MockDbContext(mockPaises.Object, mockConsumos.Object);

            systemUnderTest = new ConsumoService(dbContext, _mapper);

            ///act
            // assert
            Assert.ThrowsAsync(Is.TypeOf<ValidationException>(),
                async () =>
                {
                    await systemUnderTest.CreateConsumo(new ConsumoCommand { PaisId = idPais, Año = año, Cantidad = cantidad });
                });
        }

        [TestCase(99,2020,34.2)]
        public void CrearConsumo_PaisNoExiste_RetornaExcepcion(int idPais, int año, decimal cantidad)
        {
            //arrange
            var paises = MockDbSeed.CreateListPaises();
            var mockPaises = paises.AsQueryable().BuildMockDbSet();

            var consumos = MockDbSeed.CreateListConsumos();
            var mockConsumos = consumos.AsQueryable().BuildMockDbSet();


            mockPaises.Setup(x => x.FindAsync(It.IsAny<object[]>())).ReturnsAsync((object[] ids) =>
            {
                var id = (int)ids.First();
                return paises.FirstOrDefault(x => x.Id == id);
            });

            var dbContext = new MockDbContext(mockPaises.Object, mockConsumos.Object);

            systemUnderTest = new ConsumoService(dbContext, _mapper);

            ///act
            // assert
            Assert.ThrowsAsync(Is.TypeOf<NotFoundException>(),
                async () =>
                {
                    await systemUnderTest.CreateConsumo(new ConsumoCommand { PaisId = idPais, Año = año, Cantidad = cantidad });
                });
        }

        [TestCase(99, 2025, 45.9)]
        public void EditarConsumo_NoExistente_RetornaExcepcion(int idPais, int año, decimal cantidad)
        {
            //arrange
            var consumos = new List<Consumo>();
            var mock = consumos.AsQueryable().BuildMockDbSet();

            var dbContext = new MockDbContext(mock.Object);

            systemUnderTest = new ConsumoService(dbContext, _mapper);

            //act
            // assert
            Assert.ThrowsAsync(Is.TypeOf<NotFoundException>(),
                async () =>
                {
                    await systemUnderTest.UpdateConsumo(new ConsumoCommand { PaisId = idPais, Año = año, Cantidad = cantidad });
                });
        }

        [TestCase(1, 2020, 45.9)]
        public async Task EditarConsumo_RetornaExpected(int idPais, int año, decimal cantidad)
        {
            //arrange
            var consumos = MockDbSeed.CreateListConsumos();
            var mock = consumos.AsQueryable().BuildMockDbSet();

            var dbContext = new MockDbContext(mock.Object);

            systemUnderTest = new ConsumoService(dbContext, _mapper);

            //act
            await systemUnderTest.UpdateConsumo(new ConsumoCommand { PaisId = idPais, Año = año, Cantidad = cantidad });
            var updatedConsumo = await systemUnderTest.GetByPaisAndYear(idPais, año);

            // assert
            Assert.AreEqual(cantidad, updatedConsumo.Cantidad);
        }

        [TestCase(99, 2025)]
        public void EliminarConsumo_NoExistente_RetornaExcepcion(int idPais, int año)
        {
            //arrange
            var consumos = new List<Consumo>();
            var mock = consumos.AsQueryable().BuildMockDbSet();

            var dbContext = new MockDbContext(mock.Object);

            systemUnderTest = new ConsumoService(dbContext, _mapper);

            //act
            // assert
            Assert.ThrowsAsync(Is.TypeOf<NotFoundException>(),
                async () =>
                {
                    await systemUnderTest.DeleteConsumo(idPais, año);
                });
        }

        [TestCase(1, 2020)]
        public async Task EliminarConsumo_RetornaExpected(int idPais, int año)
        {
            //arrange
            var consumosOriginal = MockDbSeed.CreateListConsumos();
            var consumos = MockDbSeed.CreateListConsumos();
            var mock = consumos.AsQueryable().BuildMockDbSet();
            mock.Setup(m => m.Remove(It.IsAny<Consumo>())).Callback<Consumo>((entity) => consumos.Remove(entity));


            var dbContext = new MockDbContext(mock.Object);

            systemUnderTest = new ConsumoService(dbContext, _mapper);

            //act
            await systemUnderTest.DeleteConsumo(idPais, año);

            // assert
            Assert.AreEqual(consumosOriginal.Count() - 1, consumos.Count());
        }
    }
}