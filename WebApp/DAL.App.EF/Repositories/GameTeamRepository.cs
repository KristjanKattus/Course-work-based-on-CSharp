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

namespace DAL.App.EF.Repositories
{
    public class GameTeamRepository : BaseRepository<DAL.App.DTO.GameTeam, Domain.App.Game_Team, AppDbContext>, IGameTeamRepository
    {
        public GameTeamRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new GameTeamMapper(mapper))
        {
        }

        public override async Task<IEnumerable<GameTeam>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);

            var resQuery = query
                .Include(g => g.Game)
                .Include(g => g.Team)
                .Select(g => Mapper.Map(g));

            var res = await resQuery.ToListAsync();

            return res!;
        }

        public override async Task<GameTeam?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = InitializeQuery();

            var resQuery = query
                .Include(g => g.Team);
                

            var res = Mapper.Map(await resQuery.FirstOrDefaultAsync(g => g.Id == id));

            return res;
        }
        
        public async Task<IEnumerable<GameTeam>> GetAllTeamGamesAsync(Guid teamId)
        {
            var query = InitializeQuery();

            var resQuery = query
                .Where(x => x.TeamId == teamId)
                .Select(x => Mapper.Map(x));

            return (await resQuery.ToListAsync())!;
        }

        public async Task<IEnumerable<GameTeam>> GetAllTeamGamesWithGameIdAsync(Guid gameId, bool noTracking = true)
        {
            var query = InitializeQuery();
            

            var resQuery = query
                .Include(g => g.Game)
                .Include(g => g.Team)
                .Where(g => g.GameId == gameId)
                .Select(g => Mapper.Map(g));

            
            return (await resQuery.ToListAsync())!;
        }

        public async Task<GameTeam> FirstOrDefaultWithGameIdAsync(Guid id, bool homeTeam)
        {
            var query = InitializeQuery();
            var resQuery = query
                .Include(x => x.Team)
                .Where(g => g.Hometeam == homeTeam);
                

            var res = Mapper.Map(await resQuery.FirstOrDefaultAsync(x => x.GameId == id));
            return res!;
        }
    }
}