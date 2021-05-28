using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Base.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF;
using DAL.App.EF.AppDataInit;
using DAL.App.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;
using Person = BLL.App.DTO.Person;

namespace TestProject.UnitTests
{
    public class PersonBaseServiceTests
    {
        private readonly BaseEntityService<IAppUnitOfWork, IPersonRepository, BLL.App.DTO.Person, DAL.App.DTO.Person> _baseService;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly AppDbContext _ctx;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        
        // ARRANGE - common
        public PersonBaseServiceTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            
            var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionBuilder
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging(false);
            _ctx = new AppDbContext(optionBuilder.Options);
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();

            var mapperConf = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DAL.App.DTO.MappingProfiles.AutoMapperProfile>(); 
                cfg.AddProfile<BLL.App.DTO.MappingProfiles.AutoMapperProfile>(); 
                cfg.AddProfile<DTO.App.V1.MappingProfiles.AutoMapperProfile>(); 

            });
            
            _mapper = mapperConf.CreateMapper();
            var uow = new AppUnitOfWork(_ctx, _mapper);
            var personRepository = new PersonRepository(_ctx, _mapper);
            var personMapper = new BLL.App.Mappers.PersonMapper(_mapper);

            _baseService = new BaseEntityService<IAppUnitOfWork, IPersonRepository, BLL.App.DTO.Person, DAL.App.DTO.Person>
                    (uow, personRepository, personMapper);
            
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            _logger = loggerFactory.CreateLogger<ILogger<PersonServiceTests>>();
        }


        [Fact]
        public async Task Add()
        {
            // ARRANGE
            var person = await AddData();
            
            // ASSERT
            Assert.NotNull(person);
            Assert.Equal("Karl", person!.Firstname);
            Assert.Equal("Tönn", person!.Lastname);
            Assert.Equal("468464363471", person!.IdentificationCode);
        }
        
        
        [Fact]
        public async Task Get_All()
        {
            // ARRANGE
            DataInit.SeedData(_ctx, _logger);

            // ACT
            var persons = (await _baseService.GetAllAsync()).ToList();
            
            // ASSERT
            Assert.NotNull(persons);
            Assert.Equal(10, persons.Count);
            Assert.Equal("Laine", persons.First().Firstname);
        }
        
        [Fact]
        public async Task Exists()
        {
            // ARRANGE
            DataInit.SeedData(_ctx, _logger);

            // ACT
            var person = await AddData();
            
            // ASSERT
            Assert.NotNull(person);
            var exists = await _baseService.ExistsAsync(person!.Id);
            Assert.True(exists);
        }
        
        [Fact]
        public async Task First_Or_Default()
        {
            // ARRANGE & ACT & ASSERT
            var person = await _baseService.FirstOrDefaultAsync(Guid.NewGuid());
            Assert.Null(person);
            
            var personDomain = await AddData();
            Assert.NotNull(personDomain);
            
            person = await _baseService.FirstOrDefaultAsync(personDomain!.Id);
            Assert.NotNull(person);
            Assert.Equal(personDomain.FullName, person!.FullName);
        }
        
        [Fact]
        public async Task Remove()
        {
            // ARRANGE
            DataInit.SeedData(_ctx, _logger);
            _ctx.ChangeTracker.Clear();

            // ACT
            var person = (await _baseService.GetAllAsync()).FirstOrDefault();
            Assert.NotNull(person);
            _baseService.Remove(person!);
            await _ctx.SaveChangesAsync();
            var personsCount = await _ctx.Person.CountAsync();
            
            // ASSERT
            Assert.Equal(9, personsCount);
        }
        
        [Fact]
        public async Task Update()
        {
            // ARRANGE
            var person = await AddData();
            _ctx.ChangeTracker.Clear();
            
            // ACT
            Assert.NotNull(person);
            person!.Firstname = "Jaanus";
            _baseService.Update(person);
            await _ctx.SaveChangesAsync();
            var updatedPerson = await _ctx.Person.FirstOrDefaultAsync();
            
            // ASSERT
            Assert.NotEqual("Karl", updatedPerson.Firstname);
            Assert.Equal("Jaanus", updatedPerson.Firstname);
        }
        
        private async Task<Person?> AddData()
        {
            var person = new BLL.App.DTO.Person()
            {
                Firstname = "Karl",
                Lastname = "Tönn",
                IdentificationCode = "468464363471",
            };
            
            _baseService.Add(person);
            
            await _ctx.SaveChangesAsync();

            return (await _baseService.GetAllAsync()).FirstOrDefault();
        }

    }

}