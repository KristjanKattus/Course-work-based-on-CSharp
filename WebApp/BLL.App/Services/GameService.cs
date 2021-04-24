using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class GameService: BaseEntityService<IAppUnitOfWork, IGameRepository, BLLAppDTO.Game, DALAppDTO.Game>, IGameService
    {
        public GameService(IAppUnitOfWork serviceUow, IGameRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new GameMapper(mapper))
        {
        }
    }
}