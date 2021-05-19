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
    public class GameTeamService: BaseEntityService<IAppUnitOfWork, IGameTeamRepository, BLLAppDTO.GameTeam, DALAppDTO.GameTeam>, IGameTeamService
    {
        public GameTeamService(IAppUnitOfWork serviceUow, IGameTeamRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new GameTeamMapper(mapper))
        {
        }

        public async Task<IEnumerable<BLLAppDTO.GameTeam>> GetAllTeamGamesAsync(Guid teamId)
        {
            return (await ServiceRepository.GetAllTeamGamesAsync(teamId)).Select(x => Mapper.Map(x))!;
        }
    }
}