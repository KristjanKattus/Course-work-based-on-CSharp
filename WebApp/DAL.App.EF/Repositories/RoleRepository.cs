using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class RoleRepository : BaseRepository<DAL.App.DTO.Role, Domain.App.Role, AppDbContext>, IRoleRepository
    {
        public RoleRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new RoleMapper(mapper))
        {
        }
    }
}