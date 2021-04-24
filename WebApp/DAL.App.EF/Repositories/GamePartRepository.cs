using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;


namespace DAL.App.EF.Repositories
{
    public class GamePartRepository : BaseRepository<DAL.App.DTO.GamePart, Domain.App.Game_Part, AppDbContext>, IGamePartRepository
    {
        public GamePartRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new GamePartMapper(mapper))
        {
        }
    }
}