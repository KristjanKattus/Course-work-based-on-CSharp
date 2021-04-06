using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class TeamRepository : BaseRepository<Team, AppDbContext>, ITeamRepository
    {
        public TeamRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}