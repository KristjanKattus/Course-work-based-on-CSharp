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
    public class StadiumAreaRepository : BaseRepository<DAL.App.DTO.StadiumArea, Domain.App.Stadium_Area, AppDbContext>, IStadiumAreaRepository
    {
        public StadiumAreaRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new StadiumAreaMapper(mapper))
        {
        }


        public override async Task<IEnumerable<StadiumArea>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);

            var resQuery = query
                .Include(s => s.Stadiums)
                .Select(s => Mapper.Map(s));

            var res = await resQuery.ToListAsync();

            return res!;
        }

        public override async Task<StadiumArea?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);

            var resQuery = query
                .Include(s => s.Stadiums);
                

            var res = Mapper.Map(await resQuery.FirstOrDefaultAsync(s => s.Id == id));

            return res!;
        }
    }
}