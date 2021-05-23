using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;
using Game = DAL.App.DTO.Game;

namespace DAL.App.EF.Repositories
{
    public class GameEventRepository : BaseRepository<DAL.App.DTO.GameEvent, Domain.App.Game_Event, AppDbContext>, IGameEventRepository
    {
        public GameEventRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new GameEventMapper(mapper))
        {
        }

        public override async Task<IEnumerable<GameEvent>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);

            var resQuery = query
                .Include(g => g.Game)
                .Include(g => g.GameTeamList)
                .Include(g => g.GamePart)
                .Include(g => g.EventType)
                .Select(g => Mapper.Map(g));


            var res = await resQuery.ToListAsync();
                
            return res!;
        }

        public override async Task<GameEvent?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);

            var resQuery = query
                .Include(g => g.Game)
                .Include(g => g.GameTeamList)
                .Include(g => g.GamePart)
                .Include(g => g.EventType);

            var res = Mapper.Map(await resQuery.FirstOrDefaultAsync(g => g.Id == id));

            return res;
        }

        public async Task<IEnumerable<GameEvent>> GetWithGameIdAsync(Guid gameId)
        {
            var query = InitializeQuery();

            var resQuery = query
                .Include(g => g.Game)
                .Include(g => g.GameTeamList)
                .Include(g => g.GamePart)
                .Include(g => g.EventType)
                .Where(g => g.GameId == gameId)
                .Select(g => Mapper.Map(g));


            var res = await resQuery.ToListAsync();
                
            return res!;
        }
    }
}