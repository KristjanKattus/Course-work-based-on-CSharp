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
using Role = DAL.App.DTO.Role;

namespace DAL.App.EF.Repositories
{
    public class RoleRepository : BaseRepository<DAL.App.DTO.Role, Domain.App.Role, AppDbContext>, IRoleRepository
    {
        public RoleRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new RoleMapper(mapper))
        {
        }

        public override async Task<Role?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);
            
            var resQuery = query
                .Include(c => c.Name)
                .ThenInclude(t => t!.Translations);
            var res = await resQuery.FirstOrDefaultAsync(m => m.Id == id);
            return Mapper.Map(res);
        }

        public override async Task<IEnumerable<Role>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);
            
            var resQuery = query
                .Include(c => c.Name)
                .ThenInclude(t => t!.Translations)
                .Select(x => Mapper.Map(x));

            var res = await resQuery.ToListAsync();
            return res!;
        }
    }
}