using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class GameEventRepository : BaseRepository<DAL.App.DTO.GameEvent, Domain.App.Game_Event, AppDbContext>, IGameEventRepository
    {
        public GameEventRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new GameEventMapper(mapper))
        {
        }
    }
}