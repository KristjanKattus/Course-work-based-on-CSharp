using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class GamePersonnelRepository : BaseRepository<Game_Personnel, AppDbContext>, IGamePersonnelRepository
    {
        public GamePersonnelRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}