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
using Game = Domain.App.Game;

namespace DAL.App.EF.Repositories
{
    public class GameRepository : BaseRepository<DAL.App.DTO.Game, Domain.App.Game, AppDbContext>, IGameRepository
    {
        public GameRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new GameMapper(mapper))
        {
        }


        public override async Task<IEnumerable<DTO.Game>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);

            var resQuery = query
                .Include(g => g.Stadium)
                .Include(g => g.GameEvents)
                .Select(g => Mapper.Map(g));

            var res = await resQuery.ToListAsync();
            return res!;
        }

        public override async Task<DTO.Game?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);

            var resQuery = query;
                // .Include(g => g.Stadium)
                // .Include(x => x.GameEvents)
                // .ThenInclude(x => x.EventType)
                //         .ThenInclude(c => c!.Name)
                //             .ThenInclude(t => t!.Translations)
                // .Include(x => x.GameEvents)
                //     .ThenInclude(x => x.GameTeamList)
                //         .ThenInclude(x=> x!.TeamPerson)
                //             .ThenInclude(x => x!.Person)
                // .Include(x => x.GameEvents)
                //      .ThenInclude(x => x.GameTeamList)
                //         .ThenInclude(x=> x!.TeamPerson)
                //             .ThenInclude(x => x!.Team);

            var res = Mapper.Map(await resQuery.FirstOrDefaultAsync(m => m.Id == id));
            return res!;
        }

        public async Task<IEnumerable<DTO.Game>> GetAllGamesWithLeagueIdAsync(Guid leagueId)
        {
            var query = InitializeQuery();

            var resQuery = query
                // .Include(g => g.Stadium)
                .Where(g => g.LeagueId == leagueId)
                .Select(g => Mapper.Map(g));

            var res = await resQuery.ToListAsync();
            return res!;
        }

        public async Task<DTO.Game> FirstOrDefaultAsyncCustom(Guid gameId)
        {
            var query = InitializeQuery();

            var resQuery = query
            .Include(g => g.Stadium)
            .Include(x => x.GameEvents)
            .ThenInclude(x => x.EventType)
                    .ThenInclude(c => c!.Name)
                        .ThenInclude(t => t!.Translations)
            .Include(x => x.GameEvents)
                .ThenInclude(x => x.GameTeamList)
                    .ThenInclude(x=> x!.TeamPerson)
                        .ThenInclude(x => x!.Person)
            .Include(x => x.GameEvents)
                 .ThenInclude(x => x.GameTeamList)
                    .ThenInclude(x=> x!.TeamPerson)
                        .ThenInclude(x => x!.Team);

            var res = Mapper.Map(await resQuery.FirstOrDefaultAsync(m => m.Id == gameId));
            return res!;
        }
    }
}