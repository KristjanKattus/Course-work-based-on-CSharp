using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;
using Game = Domain.App.Game;

namespace DAL.App.EF.Repositories
{
    public class GameRepository : BaseRepository<DAL.App.DTO.Game, Domain.App.Game, AppDbContext>, IGameRepository
    {
        public GameRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new GameMapper(mapper))
        {
        }
        
        
        // public override async Task GetAllAsync(Guid userId, bool noTracking)
        // {
        //     var query = InitializeQuery();
        //     
        //     var resQuery = query
        //         .Include(g => g.Stadium)
        //         .Include(g => g.GameEvents)
        //         .ThenInclude(ge => ge.)
                
        // }
    }
}