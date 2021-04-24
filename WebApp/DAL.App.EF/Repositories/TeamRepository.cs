using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class TeamRepository : BaseRepository<DAL.App.DTO.Team, Domain.App.Team, AppDbContext>, ITeamRepository
    {
        public TeamRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new TeamMapper(mapper))
        {
        }
    }
}