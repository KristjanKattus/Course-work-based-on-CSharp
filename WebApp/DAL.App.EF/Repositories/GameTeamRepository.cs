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
                .Include(g => g.Team)
                .Include(g => g.Team)
                .Select(g => Mapper.Map(g));

            var res = await resQuery.ToListAsync();

            return res!;
        }

        public override async Task<GameTeam?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);

            var resQuery = query
                .Include(g => g.Team)
                .Include(g => g.Team);
                

            var res = Mapper.Map(await resQuery.FirstOrDefaultAsync(g => g.Id == id));

            return res;
        }
    }
}