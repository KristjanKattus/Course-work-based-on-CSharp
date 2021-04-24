using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class StadiumAreaRepository : BaseRepository<DAL.App.DTO.StadiumArea, Domain.App.Stadium_Area, AppDbContext>, IStadiumAreaRepository
    {
        public StadiumAreaRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new StadiumAreaMapper(mapper))
        {
            
        }
    }
}