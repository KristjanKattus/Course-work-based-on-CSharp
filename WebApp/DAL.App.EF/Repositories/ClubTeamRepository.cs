using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
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

        // public override async Task<IEnumerable<DAL.App.DTO.ClubTeam>> GetAllAsync(Guid userId = default, bool noTracking = true)
        // {
        //     // var query = InitializeQuery(userId, noTracking);
        //     //
        //     // var resQuery = query
        //     //     .Include(x => x.Club)
        //     //     .Where(c => c.Id)
        // }
    }
}