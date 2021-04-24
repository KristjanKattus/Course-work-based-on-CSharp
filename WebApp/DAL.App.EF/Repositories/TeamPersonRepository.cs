using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class TeamPersonRepository : BaseRepository<DAL.App.DTO.TeamPerson, Domain.App.Team_Person, AppDbContext>, ITeamPersonRepository
    {
        public TeamPersonRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new TeamPersonMapper(mapper))
        {
        }
    }
}