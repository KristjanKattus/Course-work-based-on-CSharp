using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF.Repositories
{
    public class ClubTeamRepository : BaseRepository<DAL.App.DTO.ClubTeam, Domain.App.Club_Team, AppDbContext>, IClubTeamRepository
    {
        public ClubTeamRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new ClubTeamMapper(mapper))
        {
        }

        
        public override async Task<IEnumerable<ClubTeam>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);

            var resQuery = query
                .Include(c => c.Club)
                .Select(x => Mapper.Map(x));

            var res = await resQuery.ToListAsync();
            return res!;
        }

        public override async Task<ClubTeam?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);

            var resQuery = query
                .Include(c => c.Club);

            var res = Mapper.Map(await resQuery.FirstOrDefaultAsync(m => m.Id == id));
            return res;
        }


        public async Task<ClubTeam> GetClubWithTeamIdAsync(Guid teamId)
        {
            var query = InitializeQuery();

            var resQuery = query
                .Include(c => c.Club)
                .Where(x => x.TeamId == teamId);

            var res = Mapper.Map(await resQuery.FirstOrDefaultAsync());
            return res!;
        }
    }
}