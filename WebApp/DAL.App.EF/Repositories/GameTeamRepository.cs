using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class GameTeamRepository : BaseRepository<Game_Team, AppDbContext>, IGameTeamRepository
    {
        public GameTeamRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}