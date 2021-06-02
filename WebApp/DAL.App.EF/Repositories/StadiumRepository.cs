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
using Stadium = DAL.App.DTO.Stadium;

namespace DAL.App.EF.Repositories
{
    public class StadiumRepository : BaseRepository<DAL.App.DTO.Stadium, Domain.App.Stadium, AppDbContext>, IStadiumRepository
    {
        public StadiumRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new StadiumMapper(mapper))
        {
        }


        public override async Task<IEnumerable<Stadium>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);

            var resQuery = query
                .Include(s => s.StadiumArea)
                .Include(s => s.Games)
                .Select(s => Mapper.Map(s));

            var res = await resQuery.ToListAsync();

            return res!;
        }
    }
}