using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class StadiumRepository : BaseRepository<DAL.App.DTO.Stadium, Domain.App.Stadium, AppDbContext>, IStadiumRepository
    {
        public StadiumRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new StadiumMapper(mapper))
        {
        }
    }
}