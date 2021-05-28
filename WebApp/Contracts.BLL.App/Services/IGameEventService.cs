using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IGameEventService : IBaseEntityService<BLLAppDTO.GameEvent, DALAppDTO.GameEvent>, IGameEventRepositoryCustom<BLLAppDTO.GameEvent>
    {
    }

    public interface IGameEventRepositoryCustom<TEntity>
    {
        public Task<IEnumerable<BLLAppDTO.GameEvent>> GetWithGameIdAsync(Guid gameId);

        public Task UpdateGameTeamAsync(Guid gameEventId);

        public Task DeleteGameEventAsync(Guid gameEventId);

        public Task<IEnumerable<BLLAppDTO.GameTeam>> GetUpdateTeams(Guid gameEventId, IMapper mapper);
    }
}