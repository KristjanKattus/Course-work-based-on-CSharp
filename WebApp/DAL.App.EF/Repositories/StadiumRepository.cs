using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class StadiumRepository : BaseRepository<Stadium, AppDbContext>, IStadiumRepository
    {
        public StadiumRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}