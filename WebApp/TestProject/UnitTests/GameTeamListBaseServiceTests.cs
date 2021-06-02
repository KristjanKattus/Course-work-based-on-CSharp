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
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using GameTeamList = BLL.App.DTO.GameTeamList;

namespace TestProject.UnitTests
{
    public class GameTeamListBaseServiceTests
    {
        private readonly BaseEntityService<IAppUnitOfWork, IGameTeamListRepository, GameTeamList, DAL.App.DTO.GameTeamList> _baseService;

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly AppDbContext _ctx;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;


        // ARRANGE - common
        public GameTeamListBaseServiceTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;

            // set up db context for testing - using InMemory db engine
            var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            // provide new random database name here
            // or parallel test methods will conflict each other
            optionBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _ctx = new AppDbContext(optionBuilder.Options);
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
            
            var mapperConf = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DAL.App.DTO.MappingProfiles.AutoMapperProfile>(); 
                cfg.AddProfile<BLL.App.DTO.MappingProfiles.AutoMapperProfile>(); 
                cfg.AddProfile<PublicApi.DTO.v1.MappingProfiles.AutoMapperProfile>(); 

            });

            _mapper = mapperConf.CreateMapper();
            var uow = new AppUnitOfWork(_ctx, _mapper);
            var gameTeamListRepository = new GameTeamListRepository(_ctx, _mapper);
            var gameTeamListMapper = new BLL.App.Mappers.GameTeamListMapper(_mapper);

            _baseService =
                new BaseEntityService<IAppUnitOfWork, IGameTeamListRepository, BLL.App.DTO.GameTeamList, DAL.App.DTO.GameTeamList>
                    (uow, gameTeamListRepository, gameTeamListMapper);

            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            _logger = loggerFactory.CreateLogger<ILogger<GameTeamListBaseServiceTests>>();
        }


        [Fact]
        public async Task Add()
        {
            // ARRANGE
            
            var gameTeamId = Guid.NewGuid();
            var gameTeamList = new GameTeamList()
            {
                GameTeamId = gameTeamId,
                TeamPersonId = Guid.NewGuid(),
                Staff = true,
                InStartingLineup = true,
                PersonId = Guid.NewGuid()
            };
            var added = _baseService.Add(gameTeamList);
            await _ctx.SaveChangesAsync();
            
            // ASSERT
            Assert.NotNull(added);
            Assert.True(added.Staff);
            Assert.True(added.InStartingLineup);
            Assert.Equal(gameTeamId, added.GameTeamId);
        }
        
        
        [Fact]
        public async Task Get_All()
        {
            // ARRANGE
            
            GameTeamList? gm = null;
            for (int i = 0; i < 5; i++)
            {
                var gameTeamList = new GameTeamList()
                {
                    GameTeamId = Guid.NewGuid(),
                    TeamPersonId = Guid.NewGuid(),
                    Staff = true,
                    InStartingLineup = true,
                    PersonId = Guid.NewGuid()
                };
                gm = _baseService.Add(gameTeamList);
                
            }
            await _ctx.SaveChangesAsync();
            
            // ACT
            var gameTeamLists = (await _baseService.GetAllAsync()).ToList();

            // ASSERT
            Assert.NotNull(gameTeamLists);
            Assert.Equal(5, gameTeamLists.Count);
        }
        
           
       [Fact]
       public async Task Exists()
       {
           // ARRANGE
           
           var gameTeamId = Guid.NewGuid();
           var gameTeamList = new GameTeamList()
           {
               GameTeamId = gameTeamId,
               TeamPersonId = Guid.NewGuid(),
               Staff = true,
               InStartingLineup = true,
               PersonId = Guid.NewGuid()
           };
           var added = _baseService.Add(gameTeamList);
           await _ctx.SaveChangesAsync();;
           
           // ASSERT
           Assert.NotNull(added);
           var exists = await _baseService.ExistsAsync(added!.Id);
           Assert.True(exists);
       }
         
           [Fact]
           public async Task First_Or_Default()
           {
               // ARRANGE
               
               var gameTeamId = Guid.NewGuid();
               var gameTeamList = new GameTeamList()
               {
                   GameTeamId = gameTeamId,
                   TeamPersonId = Guid.NewGuid(),
                   Staff = true,
                   InStartingLineup = true,
                   PersonId = Guid.NewGuid()
               };
               var added = _baseService.Add(gameTeamList);
               await _ctx.SaveChangesAsync();
               
               var newgameTeamList = await _baseService.FirstOrDefaultAsync(added!.Id);
               Assert.NotNull(newgameTeamList);
               Assert.Equal(newgameTeamList!.GameTeamId, gameTeamList!.GameTeamId);
               Assert.Equal(newgameTeamList!.TeamPersonId, gameTeamList!.TeamPersonId);
               Assert.Equal(newgameTeamList!.Staff, gameTeamList!.Staff);
               Assert.Equal(newgameTeamList!.InStartingLineup, gameTeamList!.InStartingLineup);
               Assert.Equal(newgameTeamList!.PersonId, gameTeamList!.PersonId);
               
           }
      
           [Fact]
           public async Task Remove()
           {
               // ARRANGE
               
               
               var gameTeamId = Guid.NewGuid();
               var gameTeamList = new GameTeamList()
               {
                   GameTeamId = gameTeamId,
                   TeamPersonId = Guid.NewGuid(),
                   Staff = true,
                   InStartingLineup = true,
                   PersonId = Guid.NewGuid()
               };
               var added = _baseService.Add(gameTeamList);
               await _ctx.SaveChangesAsync();
               Assert.NotNull(added);
               _ctx.ChangeTracker.Clear();
               await _baseService.RemoveAsync(added.Id!);
               
               await _ctx.SaveChangesAsync();
               var GTLCount = await _baseService.GetAllAsync();
               
               // ASSERT
               Assert.Empty(GTLCount);
           }

           [Fact]
           public async Task Update()
           {
               // ARRANGE
               
               _ctx.ChangeTracker.Clear();
               var gameTeamId = Guid.NewGuid();
               var gameTeamList = new GameTeamList()
               {
                   GameTeamId = gameTeamId,
                   TeamPersonId = Guid.NewGuid(),
                   Staff = true,
                   InStartingLineup = true,
                   PersonId = Guid.NewGuid()
               };
               var added = _baseService.Add(gameTeamList);
               await _ctx.SaveChangesAsync();
               _ctx.ChangeTracker.Clear();
               Assert.NotNull(gameTeamList);
               var guid = new Guid();
               added.PersonId = guid;
               added.Staff = false;
               var update = _baseService.Update(added);
               await _ctx.SaveChangesAsync();
               var updatedPerson = await _ctx.GameTeamLists.FirstOrDefaultAsync();

               // ASSERT
               Assert.NotEqual(gameTeamList.Staff, update.Staff);
               Assert.NotEqual(gameTeamList.PersonId, update.PersonId);
           }
    }
}