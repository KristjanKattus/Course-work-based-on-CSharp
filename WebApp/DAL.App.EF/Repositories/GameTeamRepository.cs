using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class GameTeamRepository : BaseRepository<DAL.App.DTO.GameTeam, Domain.App.Game_Team, AppDbContext>, IGameTeamRepository
    {
        public GameTeamRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new GameTeamMapper(mapper))
        {
        }
    }
}