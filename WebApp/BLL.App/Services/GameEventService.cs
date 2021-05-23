using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<BLLAppDTO.GameEvent>> GetWithGameIdAsync(Guid gameId)
        {
            return (await ServiceRepository.GetWithGameIdAsync(gameId)).Select(x => Mapper.Map(x))!;
        }
    }
}