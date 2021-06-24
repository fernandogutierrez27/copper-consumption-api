using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CopperConsumption.Application.Common.Exceptions;
using CopperConsumption.Application.Common.Interfaces;
using CopperConsumption.Application.Common.Mappings;
using CopperConsumption.Application.Paises;
using CopperConsumption.Application.UnitTests.MockDatabase;
using CopperConsumption.Domain.Entities;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;

namespace CopperConsumption.Application.UnitTests.Tests
{
    [TestFixture]
    public class PaisServiceTests
    {
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _configuration;
        private readonly Mock<ICopperConsumptionDbContext> mockDbContext;

        PaisService systemUnderTest;

        public PaisServiceTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [TestCase("Chile")]
        public async Task CrearPais_Nuevo_RetornaExpected(string nombrePais)
        {
            //arrange
            var paisesOrginal = MockDbSeed.CreateListPaises();
            var paises = MockDbSeed.CreateListPaises();
            var mock = paises.AsQueryable().BuildMockDbSet();

            mock.Setup(m => m.Add(It.IsAny<Pais>())).Callback<Pais>((entity) => paises.Add(entity));

            var dbContext = new MockDbContext(mock.Object);

            systemUnderTest = new PaisService(dbContext, _mapper);

            //act
            var result = await systemUnderTest.CreatePais(new Pais { Nombre = nombrePais });

            // assert
            Assert.AreEqual(paisesOrginal.Count() + 1, paises.Count());
        }

        [TestCase("Alemania")]
        public void CrearPais_Existente_RetornaExcepcion(string nombrePais)
        {
            //arrange
            var paises = MockDbSeed.CreateListPaises();
            var mock = paises.AsQueryable().BuildMockDbSet();

            mock.Setup(m => m.Add(It.IsAny<Pais>())).Callback<Pais>((entity) => paises.Add(entity));

            var dbContext = new MockDbContext(mock.Object);

            systemUnderTest = new PaisService(dbContext, _mapper);

            //act
            // assert
            Assert.ThrowsAsync(Is.TypeOf<ValidationException>(),
                async () =>
                {
                    await systemUnderTest.CreatePais(new Pais { Nombre = nombrePais });
                });
        }


        [TestCase("")]
        public void CrearPais_SinNombre_RetornaExcepcion(string nombrePais)
        {
            //arrange
            var paises = MockDbSeed.CreateListPaises();
            var mock = paises.AsQueryable().BuildMockDbSet();

            mock.Setup(m => m.Add(It.IsAny<Pais>())).Callback<Pais>((entity) => paises.Add(entity));

            var dbContext = new MockDbContext(mock.Object);

            systemUnderTest = new PaisService(dbContext, _mapper);

            //act
            // assert
            Assert.ThrowsAsync(Is.TypeOf<ValidationException>(),
                async () =>
                {
                    await systemUnderTest.CreatePais(new Pais { Nombre = nombrePais });
                });
        }

        [TestCase(99, "Chile")]
        public void EditarPais_NoExistente_RetornaExcepcion(int idPais, string nombrePais)
        {
            //arrange
            var paises = new List<Pais>();
            var mock = paises.AsQueryable().BuildMockDbSet();

            mock.Setup(x => x.FindAsync(It.IsAny<object[]>())).ReturnsAsync((object[] ids) =>
            {
                var id = (int)ids.First();
                return paises.FirstOrDefault(x => x.Id == id);
            });

            var dbContext = new MockDbContext(mock.Object);

            systemUnderTest = new PaisService(dbContext, _mapper);

            //act
            // assert
            Assert.ThrowsAsync(Is.TypeOf<NotFoundException>(),
                async () =>
                {
                    await systemUnderTest.UpdatePais(new Pais { Id = idPais, Nombre = nombrePais });
                });
        }

        [TestCase(1, "Noruega")]
        public async Task EditarPais_RetornaExpected(int idPais, string nombrePais)
        {
            //arrange
            var paises = MockDbSeed.CreateListPaises();
            var mock = paises.AsQueryable().BuildMockDbSet();

            mock.Setup(x => x.FindAsync(It.IsAny<object[]>())).ReturnsAsync((object[] ids) =>
            {
                var id = (int)ids.First();
                return paises.FirstOrDefault(x => x.Id == id);
            });

            var dbContext = new MockDbContext(mock.Object);

            systemUnderTest = new PaisService(dbContext, _mapper);

            //act
            await systemUnderTest.UpdatePais(new Pais { Id = idPais, Nombre = nombrePais });
            var updatedPais = await systemUnderTest.GetPaisById(idPais);

            // assert
            Assert.AreEqual(nombrePais, updatedPais.Nombre);
        }

        [TestCase(1, "")]
        public void EditarPais_NombreBlanco_RetornaExcepcion(int idPais, string nombrePais)
        {
            //arrange
            var paises = MockDbSeed.CreateListPaises();
            var mock = paises.AsQueryable().BuildMockDbSet();

            mock.Setup(x => x.FindAsync(It.IsAny<object[]>())).ReturnsAsync((object[] ids) =>
            {
                var id = (int)ids.First();
                return paises.FirstOrDefault(x => x.Id == id);
            });

            var dbContext = new MockDbContext(mock.Object);

            systemUnderTest = new PaisService(dbContext, _mapper);

            // act
            // assert
            Assert.ThrowsAsync(Is.TypeOf<ValidationException>(),
                async () =>
                {
                    await systemUnderTest.UpdatePais(new Pais { Id = idPais, Nombre = nombrePais });
                });
        }

        [TestCase(6)] // el Pais 6 no tiene consumos asociados
        public async Task EliminarPais_Consulta_RetornaDatosActualizados(int idPais)
        {
            //arrange
            var paisesInicial = MockDbSeed.CreateListPaises();
            var paises = MockDbSeed.CreateListPaises();
            var mockPaises = paises.AsQueryable().BuildMockDbSet();

            var consumos = MockDbSeed.CreateListConsumos();
            var mockConsumos = consumos.AsQueryable().BuildMockDbSet();

            mockPaises.Setup(x => x.FindAsync(It.IsAny<object[]>())).ReturnsAsync((object[] ids) =>
            {
                var id = (int)ids.First();
                return paises.FirstOrDefault(x => x.Id == id);
            });

            mockPaises.Setup(m => m.Remove(It.IsAny<Pais>())).Callback<Pais>((entity) => paises.Remove(entity));

            var dbContext = new MockDbContext(mockPaises.Object, mockConsumos.Object);

            systemUnderTest = new PaisService(dbContext, _mapper);

            //act
            await systemUnderTest.DeletePais(idPais);
            var paisesRestantes = await systemUnderTest.GetPaisesAsync();
            // assert
            Assert.AreEqual(paisesInicial.Count() - 1, paisesRestantes.Count());

        }

        [TestCase(99)]
        public void EliminarPais_NoExistente_RetornaExcepcion(int idPais)
        {
            //arrange
            var paisesInicial = MockDbSeed.CreateListPaises();
            var paises = MockDbSeed.CreateListPaises();
            var mockPaises = paises.AsQueryable().BuildMockDbSet();

            var consumos = MockDbSeed.CreateListConsumos();
            var mockConsumos = consumos.AsQueryable().BuildMockDbSet();

            mockPaises.Setup(x => x.FindAsync(It.IsAny<object[]>())).ReturnsAsync((object[] ids) =>
            {
                var id = (int)ids.First();
                return paises.FirstOrDefault(x => x.Id == id);
            });

            mockPaises.Setup(m => m.Remove(It.IsAny<Pais>())).Callback<Pais>((entity) => paises.Remove(entity));

            var dbContext = new MockDbContext(mockPaises.Object, mockConsumos.Object);

            systemUnderTest = new PaisService(dbContext, _mapper);

            // act
            // assert
            Assert.ThrowsAsync(Is.TypeOf<NotFoundException>(),
                async () =>
                {
                    await systemUnderTest.DeletePais(idPais);
                });
        }

        [TestCase(1)]
        public void EliminarPais_ConConsumos_RetornaExcepcion(int idPais)
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

            mockPaises.Setup(m => m.Remove(It.IsAny<Pais>())).Callback<Pais>((entity) => paises.Remove(entity));

            var dbContext = new MockDbContext(mockPaises.Object, mockConsumos.Object);

            systemUnderTest = new PaisService(dbContext, _mapper);

            // act
            // assert
            Assert.ThrowsAsync(Is.TypeOf<ValidationException>(),
                async () =>
                {
                    await systemUnderTest.DeletePais(idPais);
                });
        }

        [TestCase(1, "Alemania")]
        public async Task ObtenerPais_Existente_RetornaExpected(int idPais, string nombrePais)
        {
            //arrange
            var paises = MockDbSeed.CreateListPaises();
            var mockPaises = paises.AsQueryable().BuildMockDbSet();

            mockPaises.Setup(x => x.FindAsync(It.IsAny<object[]>())).ReturnsAsync((object[] ids) =>
            {
                var id = (int)ids.First();
                return paises.FirstOrDefault(x => x.Id == id);
            });

            var dbContext = new MockDbContext(mockPaises.Object);

            systemUnderTest = new PaisService(dbContext, _mapper);

            // act
            var pais = await systemUnderTest.GetPaisById(idPais);
            // assert
            Assert.AreEqual(nombrePais, pais.Nombre);
        }

        [TestCase(99)]
        public void ObtenerPais_NoExistente_RetornaExcepcion(int idPais)
        {
            //arrange
            var paises = MockDbSeed.CreateListPaises();
            var mockPaises = paises.AsQueryable().BuildMockDbSet();

            mockPaises.Setup(x => x.FindAsync(It.IsAny<object[]>())).ReturnsAsync((object[] ids) =>
            {
                var id = (int)ids.First();
                return paises.FirstOrDefault(x => x.Id == id);
            });

            var dbContext = new MockDbContext(mockPaises.Object);

            systemUnderTest = new PaisService(dbContext, _mapper);

            // act
            // assert
            Assert.ThrowsAsync(Is.TypeOf<NotFoundException>(),
                async () =>
                {
                    await systemUnderTest.GetPaisById(idPais);
                });
        }
    }
}
