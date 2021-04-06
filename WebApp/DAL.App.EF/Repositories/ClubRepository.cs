using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class ClubRepository : BaseRepository<Club, AppDbContext>, IClubRepository
    {
        public ClubRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}