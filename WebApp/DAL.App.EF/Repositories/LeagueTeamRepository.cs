using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class LeagueTeamRepository : BaseRepository<League_Team, AppDbContext>, ILeagueTeamRepository
    {
        public LeagueTeamRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}