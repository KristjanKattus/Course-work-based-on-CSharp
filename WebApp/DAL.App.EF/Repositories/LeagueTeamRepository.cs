using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class LeagueTeamRepository : BaseRepository<DAL.App.DTO.LeagueTeam, Domain.App.League_Team, AppDbContext>, ILeagueTeamRepository
    
    {
        public LeagueTeamRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new LeagueTeamMapper(mapper))
        {
        }
    }
}