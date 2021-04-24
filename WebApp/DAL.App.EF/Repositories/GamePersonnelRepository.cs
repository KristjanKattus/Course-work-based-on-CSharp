using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class GamePersonnelRepository : BaseRepository<DAL.App.DTO.GamePersonnel, Domain.App.Game_Personnel, AppDbContext>, IGamePersonnelRepository
    {
        public GamePersonnelRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new GamePersonnelMapper(mapper))
        {
        }
    }
}