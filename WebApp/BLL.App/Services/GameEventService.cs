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
    public class GameEventService: BaseEntityService<IAppUnitOfWork, IGameEventRepository, BLLAppDTO.GameEvent, DALAppDTO.GameEvent>, IGameEventService
    {
        public GameEventService(IAppUnitOfWork serviceUow, IGameEventRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new GameEventMapper(mapper))
        {
        }
    }
}