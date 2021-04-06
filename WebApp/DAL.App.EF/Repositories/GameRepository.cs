using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class GameRepository : BaseRepository<Game, AppDbContext>, IGameRepository
    {
        public GameRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}