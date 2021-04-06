
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class GamePartTypeRepository : BaseRepository<Game_Part_Type, AppDbContext>, IGamePartTypeRepository
    {
        public GamePartTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}