using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class GamePartRepository : BaseRepository<Game_Part, AppDbContext>, IGamePartRepository
    {
        public GamePartRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}