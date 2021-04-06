using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class AreaRepository : BaseRepository<Stadium_Area, AppDbContext>, IAreaRepository
    {
        public AreaRepository(AppDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}