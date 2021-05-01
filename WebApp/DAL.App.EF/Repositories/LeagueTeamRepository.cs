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
    public class LeagueTeamRepository : BaseRepository<DAL.App.DTO.LeagueTeam, Domain.App.League_Team, AppDbContext>, ILeagueTeamRepository
    
    {
        public LeagueTeamRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new LeagueTeamMapper(mapper))
        {
        }

        public override async Task<IEnumerable<LeagueTeam>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);

            var resQuery = query
                .Include(l => l.Team)
                .Include(l => l.League)
                .Select(l => Mapper.Map(l));

            var res = await resQuery.ToListAsync();

            return res!;
        }

        public override async Task<LeagueTeam?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);

            var resQuery = query
                .Include(l => l.Team)
                .Include(l => l.League);

            var res = Mapper.Map(await resQuery.FirstOrDefaultAsync(l => l.Id == id));

            return res;
        }
    }
}