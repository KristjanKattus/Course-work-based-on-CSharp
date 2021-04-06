using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class ClubTeamRepository : BaseRepository<Club_Team, AppDbContext>, IClubTeamRepository
    {
        public ClubTeamRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}