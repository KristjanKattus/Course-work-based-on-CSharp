using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;
using Team = DAL.App.DTO.Team;

namespace DAL.App.EF.Repositories
{
    public class TeamRepository : BaseRepository<DAL.App.DTO.Team, Domain.App.Team, AppDbContext>, ITeamRepository
    {
        public TeamRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new TeamMapper(mapper))
        {
        }

        public override async Task<IEnumerable<Team>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);

            var resQuery = query
                .Include(t => t.TeamPersons)
                .Select(t => Mapper.Map(t));

            var res = await resQuery.ToListAsync();

            return res!;
        }

        public override async Task<Team?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);

            var resQuery = query
                .Include(t => t.TeamPersons);
                

            var res = Mapper.Map(await resQuery.FirstOrDefaultAsync(s => s.Id == id));

            return res;
        }
    }
}