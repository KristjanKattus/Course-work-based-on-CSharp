using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.Services;
using DAL.App.EF;
using DAL.App.EF.AppDataInit;
using Domain.App;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;

namespace TestProject.UnitTests
{
    public class PersonServiceTests
    {
        private readonly PersonService _personService;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly AppDbContext _ctx;
        private readonly ILogger _logger;
        
        // ARRANGE - common
        public PersonServiceTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            
            // set up db context for testing - using InMemory db engine
            var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            // provide new random database name here
            // or parallel test methods will conflict each other
            optionBuilder
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            _ctx = new AppDbContext(optionBuilder.Options);
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();

            var mapperConf = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DAL.App.DTO.MappingProfiles.AutoMapperProfile>(); 
                cfg.AddProfile<BLL.App.DTO.MappingProfiles.AutoMapperProfile>(); 
                cfg.AddProfile<DTO.App.V1.MappingProfiles.AutoMapperProfile>(); 
            });
            var mapper = mapperConf.CreateMapper();
            
            var uow = new AppUnitOfWork(_ctx, mapper);
            
            _personService = new PersonService(uow, uow.Person, mapper);
            
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            _logger = loggerFactory.CreateLogger<ILogger<PersonServiceTests>>();
        }


        [Fact]
        public async Task Update_BloodGroup()
        {
            // ARRANGE
            var (personId, bloodGroupId) = await Seed_Data_Update_BloodGroup();
            
            // ACT
            await _personService.PutBloodGroupIfNeeded(personId, bloodGroupId);
            var person = await _ctx.Person.FirstAsync();
            
            // ASSERT
            Assert.Equal(bloodGroupId, person.BloodGroupId);
        }
        
        private async Task<(Guid, Guid)> Seed_Data_Update_BloodGroup()
        {
            var person = new Person()
            {
                Firstname = "firstname" + 1,
                Lastname = "LastName" + 1,
                IdentificationCode = "IdentificationCode" + 1
            };

            var bloodGroup = new BloodGroup()
            {
                BloodGroupValue = "BloodGroupValue" + 1
            };
            
            _ctx.Person.Add(person);
            _ctx.BloodGroup.Add(bloodGroup);

            await _ctx.SaveChangesAsync();

            var personId = person.Id;
            var bloodGroupId = bloodGroup.Id;

            _ctx.Entry(person).State = EntityState.Detached;
            _ctx.Entry(bloodGroup).State = EntityState.Detached;

            return (personId, bloodGroupId);
        }
        
        [Fact]
        public async Task Dont_Update_BloodGroup()
        {
            // ARRANGE
            var (personId, fakeBloodGroupId, realBloodGroupId) = await Seed_Data_Dont_Update_BloodGroup();
            
            // ACT
            await _personService.PutBloodGroupIfNeeded(personId, fakeBloodGroupId);
            var person = await _ctx.Person.FirstAsync();
            
            // ASSERT
            Assert.Equal(realBloodGroupId, person.BloodGroupId);
        }

        private async Task<(Guid, Guid, Guid)> Seed_Data_Dont_Update_BloodGroup()
        {
            var bloodGroupReal = new BloodGroup()
            {
                BloodGroupValue = "real"
            };
            
            _ctx.BloodGroup.Add(bloodGroupReal);

            var person = new Person()
            {
                Firstname = "firstname" + 1,
                Lastname = "LastName" + 1,
                IdentificationCode = "IdentificationCode" + 1,
                BloodGroupId = bloodGroupReal.Id
            };

            var bloodGroupFake = new BloodGroup()
            {
                BloodGroupValue = "fake"
            };

            _ctx.Person.Add(person);
            _ctx.BloodGroup.Add(bloodGroupReal);
            _ctx.BloodGroup.Add(bloodGroupFake);

            await _ctx.SaveChangesAsync();

            var personId = person.Id;
            var realBloodGroupId = bloodGroupReal.Id;
            var fakeBloodGroupId = bloodGroupFake.Id;

            _ctx.Entry(person).State = EntityState.Detached;
            _ctx.Entry(bloodGroupReal).State = EntityState.Detached;
            _ctx.Entry(bloodGroupFake).State = EntityState.Detached;

            return (personId, fakeBloodGroupId, realBloodGroupId);
        }

        [Fact]
        public async Task Get_All_Specifics_Persons_FirstName()
        {
            // ARRANGE
            DataInit.SeedData(_ctx, _logger);
            var person = new BLL.App.DTO.Person()
            {
                Firstname = "Jüri",
            };
            
            // ACT
            var persons =  await _personService.GetAllSpecificsPersonsAsync(person);
            
            // ASSERT
            Assert.Equal(3, persons.Count());
        }
        
        [Fact]
        public async Task Get_All_Specifics_Persons_FirstName_And_Lastname()
        {
            // ARRANGE
            DataInit.SeedData(_ctx, _logger);
            var person = new BLL.App.DTO.Person()
            {
                Firstname = "Jüri",
                Lastname = "Paat"
            };
            
            // ACT
            var persons =  await _personService.GetAllSpecificsPersonsAsync(person);
            
            // ASSERT
            Assert.Single(persons);
            Assert.Equal("3856735735756", persons.First().IdentificationCode);
        }
        
        [Fact]
        public async Task Get_All_Specific_Persons_By_PersonType()
        {
            // ARRANGE
            DataInit.SeedData(_ctx, _logger);

            // ACT
            var persons =  await _personService.GetAllSpecificPersonsByPersonTypeAsync("Doctor");
            
            // ASSERT
            Assert.Equal(10, persons.Count());
        }
        
        [Fact]
        public async Task Get_BloodGroup_Id_By_Specific_Person()
        {
            // ARRANGE
            DataInit.SeedData(_ctx, _logger);
            var person = await _ctx.Person.FirstAsync();

            // ACT
            var bloodGroupId =  await _personService.GetBloodGroupIdBySpecificPersonAsync(person.Id);
            
            // ASSERT
            var bloodGroup = await _ctx.BloodGroup.Where(b => b.BloodGroupValue == "AB+").FirstAsync();
            Assert.Equal(bloodGroup.Id, bloodGroupId);
        }
        
        [Fact]
        public async Task? First_Width_Include_Async()
        {
            // ARRANGE
            DataInit.SeedData(_ctx, _logger);
            var person = await _ctx.Person.FirstAsync();

            // ACT
            var servicePerson =  await _personService.FirstWidthIncludeAsync(person.Id);
            
            // ASSERT
            Assert.NotNull(servicePerson);
            Assert.Equal("AB+", servicePerson!.BloodGroup!.BloodGroupValue);
            
            var culture = Thread.CurrentThread.CurrentUICulture.Name;
            
            var contactTypeValue = culture switch
            {
                "en-US" => "Doctor",
                "et-EE" => "Doktor",
                _ => "Doctor",
            };
            Assert.NotNull(contactTypeValue);
            Assert.Equal(contactTypeValue, servicePerson!.PersonType!.PersonTypeValue);
        }
        
    }

}