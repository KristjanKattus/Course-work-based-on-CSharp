using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class LeagueRepository : BaseRepository<League, AppDbContext>, ILeagueRepository
    {
        public LeagueRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}