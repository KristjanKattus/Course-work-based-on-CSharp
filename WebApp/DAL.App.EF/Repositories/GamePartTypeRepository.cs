
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;


namespace DAL.App.EF.Repositories
{
    public class GamePartTypeRepository : BaseRepository<DAL.App.DTO.GamePartType, Domain.App.Game_Part_Type, AppDbContext>, IGamePartTypeRepository
    {
        public GamePartTypeRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new GamePartTypeMapper(mapper))
        {
        }
    }
}