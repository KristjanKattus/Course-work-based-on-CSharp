using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class GameEventRepository : BaseRepository<Game_Event, AppDbContext>, IGameEventRepository
    {
        public GameEventRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}