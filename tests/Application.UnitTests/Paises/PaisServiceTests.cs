using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CopperConsumption.Application.Common.Interfaces;
using CopperConsumption.Application.Common.Mappings;
using CopperConsumption.Application.Paises;
using CopperConsumption.Application.UnitTests.MockDatabase;
using CopperConsumption.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;

namespace CopperConsumption.Application.UnitTests.Paises
{
    [TestFixture]
    public class PaisTests
    {
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _configuration;


        public PaisTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [TestCase("Chile")]
        public async Task CreatePais(string nombre)
        {
            //arrange
            var paises = new List<Pais>();
            var mock = paises.AsQueryable().BuildMockDbSet();
            mock.Setup(set => set.AddAsync(It.IsAny<Pais>(), It.IsAny<CancellationToken>()))
              .Callback((Pais entity, CancellationToken _) => paises.Add(entity));
            var dbContext = new MockDbContext(mock.Object);
            var service = new PaisService(dbContext, _mapper);
            
            //act
            await service.CreatePais(new Pais { Nombre = nombre });
            
            // assert
            var pais = await mock.Object.SingleOrDefaultAsync();
            Assert.AreEqual(nombre, pais.Nombre);
        }

        [TestCase]
        public async Task GetAllPaises()
        {
            //arrange
            var paises = CreatePaisesList();
            var paisesDbSet = paises.AsQueryable().BuildMockDbSet();
            var repository = new MockDbContext(paisesDbSet.Object);
            PaisService service = new PaisService(repository, _mapper);
            //act
            var result = await service.GetPaisesAsync();
            //assert
            Assert.AreEqual(paises.Count, result.Count);
        }

    //     [TestCase]
    //     public async Task DbSetFindAsyncUserEntity()
    //     {
    //         //arrange
    //         var userId = Guid.NewGuid();
    //         var users = new List<UserEntity>
    //   {
    //     new UserEntity
    //     {
    //       Id = Guid.NewGuid(),
    //       FirstName = "FirstName1", LastName = "LastName",
    //       DateOfBirth = DateTime.Parse("01/20/2012", UsCultureInfo.DateTimeFormat)
    //     },
    //     new UserEntity
    //     {
    //       Id = Guid.NewGuid(),
    //       FirstName = "FirstName2", LastName = "LastName",
    //       DateOfBirth = DateTime.Parse("01/20/2012", UsCultureInfo.DateTimeFormat)
    //     },
    //     new UserEntity
    //     {
    //       Id = userId,
    //       FirstName = "FirstName3", LastName = "LastName",
    //       DateOfBirth = DateTime.Parse("01/20/2012", UsCultureInfo.DateTimeFormat)
    //     },
    //     new UserEntity
    //     {
    //       Id = Guid.NewGuid(),
    //       FirstName = "FirstName3", LastName = "LastName",
    //       DateOfBirth = DateTime.Parse("03/20/2012", UsCultureInfo.DateTimeFormat)
    //     },
    //     new UserEntity
    //     {
    //       Id = Guid.NewGuid(),
    //       FirstName = "FirstName5", LastName = "LastName",
    //       DateOfBirth = DateTime.Parse("01/20/2018", UsCultureInfo.DateTimeFormat)
    //     }
    //   };

    //         var mock = users.AsQueryable().BuildMockDbSet();
    //         mock.Setup(x => x.FindAsync(It.IsAny<object[]>())).ReturnsAsync((object[] ids) =>
    //         {
    //             var id = (Guid)ids.First();
    //             return users.FirstOrDefault(x => x.Id == id);
    //         });
    //         var userRepository = new TestDbSetRepository(mock.Object);

    //         //act
    //         var result = await ((DbSet<UserEntity>)userRepository.GetQueryable()).FindAsync(userId);

    //         //assert
    //         Assert.IsNotNull(result);
    //         Assert.AreEqual("FirstName3", result.FirstName);
    //     }

    

        // [TestCase("AnyFirstName", "AnyExistLastName", "01/20/2012", "Users with DateOfBirth more than limit")]
        // [TestCase("ExistFirstName", "AnyExistLastName", "02/20/2012", "User with FirstName already exist")]
        // [TestCase("AnyFirstName", "ExistLastName", "01/20/2012", "User already exist")]
        // public void CreatePaisIfNotExist(string firstName, string lastName, DateTime dateOfBirth, string expectedError)
        // {
        //     //arrange
        //     var repository = new Mock<ICopperConsumptionDbContext>();
        //     var service = new PaisService(repository.Object, _mapper);

        //     var paises = new List<Pais>
        //     {
        // new Pais { Id = 1, Nombre = "Alemania"},
        // new Pais { Id = 2, Nombre = "Arabia Saudita"},
        // new Pais { Id = 3, Nombre = "Argentina"},
        // new Pais { Id = 4, Nombre = "Australia"},
        // new Pais { Id = 5, Nombre = "Austria"},
        //     };
        //     //expect
        //     var mock = paises.AsQueryable().BuildMock();
        //     repository.Setup(x => x.GetQueryable()).Returns(mock.Object);
        //     //act
        //     var ex = Assert.ThrowsAsync<ApplicationException>(() =>
        //       service.CreateUserIfNotExist(firstName, lastName, dateOfBirth));
        //     //assert
        //     Assert.AreEqual(expectedError, ex.Message);
        // }

        // [TestCase("01/20/2012", "06/20/2018", 5)]
        // [TestCase("01/20/2012", "06/20/2012", 4)]
        // [TestCase("01/20/2012", "02/20/2012", 3)]
        // [TestCase("01/20/2010", "02/20/2011", 0)]
        // public async Task GetUserReports(DateTime from, DateTime to, int expectedCount)
        // {
        //     //arrange
        //     var userRepository = new Mock<IUserRepository>();
        //     var service = new MyService(userRepository.Object);
        //     var users = CreateUserList();
        //     //expect
        //     var mock = users.AsQueryable().BuildMock();
        //     userRepository.Setup(x => x.GetQueryable()).Returns(mock.Object);
        //     //act
        //     var result = await service.GetUserReports(from, to);
        //     //assert
        //     Assert.AreEqual(expectedCount, result.Count);
        // }



        // [TestCase("01/20/2012", "06/20/2018", 5)]
        // [TestCase("01/20/2012", "06/20/2012", 4)]
        // [TestCase("01/20/2012", "02/20/2012", 3)]
        // [TestCase("01/20/2010", "02/20/2011", 0)]
        // public async Task GetUserReports_AutoMap(DateTime from, DateTime to, int expectedCount)
        // {
        //     //arrange
        //     var userRepository = new Mock<IUserRepository>();
        //     var service = new MyService(userRepository.Object);
        //     var users = CreateUserList();
        //     //expect
        //     var mock = users.AsQueryable().BuildMock();
        //     userRepository.Setup(x => x.GetQueryable()).Returns(mock.Object);
        //     //act
        //     var result = await service.GetUserReportsAutoMap(from, to);
        //     //assert
        //     Assert.AreEqual(expectedCount, result.Count);
        // }


        //     [TestCase("AnyFirstName", "AnyExistLastName", "01/20/2012", "Users with DateOfBirth more than limit")]
        //     [TestCase("ExistFirstName", "AnyExistLastName", "02/20/2012", "User with FirstName already exist")]
        //     [TestCase("AnyFirstName", "ExistLastName", "01/20/2012", "User already exist")]
        //     public void DbSetCreateUserIfNotExist(string firstName, string lastName, DateTime dateOfBirth, string expectedError)
        //     {
        //         //arrange
        //         var users = new List<UserEntity>
        //   {
        //     new UserEntity {LastName = "ExistLastName", DateOfBirth = DateTime.Parse("01/20/2012", UsCultureInfo.DateTimeFormat)},
        //     new UserEntity {FirstName = "ExistFirstName"},
        //     new UserEntity {DateOfBirth = DateTime.Parse("01/20/2012", UsCultureInfo.DateTimeFormat)},
        //     new UserEntity {DateOfBirth = DateTime.Parse("01/20/2012", UsCultureInfo.DateTimeFormat)},
        //     new UserEntity {DateOfBirth = DateTime.Parse("01/20/2012", UsCultureInfo.DateTimeFormat)}
        //   };
        //         var mock = users.AsQueryable().BuildMockDbSet();
        //         var userRepository = new TestDbSetRepository(mock.Object);
        //         var service = new MyService(userRepository);
        //         //act
        //         var ex = Assert.ThrowsAsync<ApplicationException>(() =>
        //           service.CreateUserIfNotExist(firstName, lastName, dateOfBirth));
        //         //assert
        //         Assert.AreEqual(expectedError, ex.Message);
        //     }



        // [TestCase("01/20/2012", "06/20/2018", 5)]
        // [TestCase("01/20/2012", "06/20/2012", 4)]
        // [TestCase("01/20/2012", "02/20/2012", 3)]
        // [TestCase("01/20/2010", "02/20/2011", 0)]
        // public async Task DbSetGetUserReports(DateTime from, DateTime to, int expectedCount)
        // {
        //     //arrange
        //     var users = CreateUserList();
        //     var mock = users.AsQueryable().BuildMockDbSet();
        //     var userRepository = new TestDbSetRepository(mock.Object);
        //     var service = new MyService(userRepository);
        //     //act
        //     var result = await service.GetUserReports(from, to);
        //     //assert
        //     Assert.AreEqual(expectedCount, result.Count);
        // }



        //     [TestCase]
        //     public async Task DbSetGetAllUserEntitiesAsync()
        //     {
        //         // arrange
        //         var users = CreateUserList();

        //         var mockDbSet = users.AsQueryable().BuildMockDbSet();
        //         var userRepository = new TestDbSetRepository(mockDbSet.Object);

        //         // act
        //         var result = await userRepository.GetAllAsync().ToListAsync();

        //         // assert
        //         Assert.AreEqual(users.Count, result.Count);
        //     }

        private static List<Pais> CreatePaisesList() => new List<Pais>
        {
            new Pais { Id = 1, Nombre = "Alemania"},
            new Pais { Id = 2, Nombre = "Arabia Saudita"},
            new Pais { Id = 3, Nombre = "Argentina"},
            new Pais { Id = 4, Nombre = "Australia"},
            new Pais { Id = 5, Nombre = "Austria"}
        };
    }
}

// {
//     public class RequestLoggerTests
//     {
//         private readonly Mock<ILogger<CreateTodoItemCommand>> _logger;
//         private readonly Mock<ICurrentUserService> _currentUserService;
//         private readonly Mock<IIdentityService> _identityService;


//         public RequestLoggerTests()
//         {
//             _logger = new Mock<ILogger<CreateTodoItemCommand>>();

//             _currentUserService = new Mock<ICurrentUserService>();

//             _identityService = new Mock<IIdentityService>();
//         }

//         [Test]
//         public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
//         {
//             _currentUserService.Setup(x => x.UserId).Returns("Administrator");

//             var requestLogger = new LoggingBehaviour<CreateTodoItemCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

//             await requestLogger.Process(new CreateTodoItemCommand { ListId = 1, Title = "title" }, new CancellationToken());

//             _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
//         }

//         [Test]
//         public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
//         {
//             var requestLogger = new LoggingBehaviour<CreateTodoItemCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

//             await requestLogger.Process(new CreateTodoItemCommand { ListId = 1, Title = "title" }, new CancellationToken());

//             _identityService.Verify(i => i.GetUserNameAsync(null), Times.Never);
//         }
//     }
// }
