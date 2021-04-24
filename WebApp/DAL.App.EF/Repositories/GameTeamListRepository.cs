using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;


namespace DAL.App.EF.Repositories
{
    public class GameTeamListRepository : BaseRepository<DAL.App.DTO.GameTeamList, Domain.App.Game_Team_List, AppDbContext>, IGameTeamListRepository

    {
        public GameTeamListRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new GameTeamListMapper(mapper))
        {
        }
    }
}