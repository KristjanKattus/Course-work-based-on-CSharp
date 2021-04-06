using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class TeamPersonRepository : BaseRepository<Team_Person, AppDbContext>, ITeamPersonRepository
    {
        public TeamPersonRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}