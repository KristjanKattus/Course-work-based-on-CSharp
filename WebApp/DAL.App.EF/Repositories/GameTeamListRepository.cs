using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class GameTeamListRepository : BaseRepository<Game_Team_List, AppDbContext>, IGameTeamListRepository

    {
        public GameTeamListRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}