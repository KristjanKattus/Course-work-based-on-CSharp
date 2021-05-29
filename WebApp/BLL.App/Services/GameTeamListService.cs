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
    public class GameTeamListService: BaseEntityService<IAppUnitOfWork, IGameTeamListRepository, BLLAppDTO.GameTeamList, DALAppDTO.GameTeamList>, IGameTeamListService
    {
        public GameTeamListService(IAppUnitOfWork serviceUow, IGameTeamListRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new GameTeamListMapper(mapper))
        {
        }

        public async Task<IEnumerable<BLLAppDTO.GameTeamList>> GetAllWithLeagueTeamIdAsync(Guid gameTeamId)
        {
            return (await ServiceRepository.GetAllWithLeagueTeamIdAsync(gameTeamId)).Select(x => Mapper.Map(x))!;
        }

        public void AddTeamPersonToList(Guid gameTeamId, Guid personId, bool inStartingLineUp = false)
        {
            var GTLEntity = new BLLAppDTO.GameTeamList
            {
                GameTeamId = gameTeamId,
                TeamPersonId = personId,
                InStartingLineup = inStartingLineUp
            };
            ServiceRepository.Add(Mapper.Map(GTLEntity)!);
            ServiceUow.SaveChangesAsync();
        }
    }
}