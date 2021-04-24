using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;


namespace DAL.App.EF.Repositories
{
    public class LeagueRepository : BaseRepository<DAL.App.DTO.League, Domain.App.League, AppDbContext>, ILeagueRepository
    {
        public LeagueRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new LeagueMapper(mapper))
        {
        }
    }
}