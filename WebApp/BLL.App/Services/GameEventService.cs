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

        public async Task UpdateGameTeamAsync(Guid gameEventId)
        {
            var gameEvent = await ServiceUow.GameEvents.FirstOrDefaultAsync(gameEventId);

            var gameTeams = (await ServiceUow.GameTeams.GetAllTeamGamesWithGameIdAsync(gameEvent!.GameId))
                .ToList();

            var eventTeam = gameTeams.First(x => x.Id == gameEvent.GameTeamList!.GameTeamId);
            var otherTeam = gameTeams.First(x => x.Id != gameEvent.GameTeamList!.GameTeamId);

            if (gameEvent.EventType!.Name == "Goal" || gameEvent.EventType!.Name == "Penalty")
            {
                eventTeam.Scored++;
                otherTeam.Conceded++;
            }else if (gameEvent.EventType!.Name == "Own goal")
            {
                eventTeam.Scored--;
                eventTeam.Conceded++;
            }
            
            ServiceUow.GameTeams.Update(eventTeam);
            ServiceUow.GameTeams.Update(otherTeam);
            await ServiceUow.SaveChangesAsync();
        }

        public async Task DeleteGameEventAsync(Guid gameEventId)
        {
            var gameEvent = await ServiceRepository.FirstOrDefaultAsync(gameEventId);

            var gameTeams = (await ServiceUow.GameTeams.GetAllTeamGamesWithGameIdAsync(gameEvent!.GameId))
                .ToList();

            var eventTeam = gameTeams.First(x => x.Id == gameEvent.GameTeamList!.GameTeamId);
            var otherTeam = gameTeams.First(x => x.Id != gameEvent.GameTeamList!.GameTeamId);

            if (gameEvent.EventType!.Name == "Goal" || gameEvent.EventType!.Name == "Penalty")
            {
                eventTeam.Scored--;
                otherTeam.Conceded--;
            }else if (gameEvent.EventType!.Name == "Own goal")
            {
                eventTeam.Conceded--;
            }

            ServiceUow.GameTeams.Update(eventTeam);
            await ServiceUow.SaveChangesAsync();
            ServiceUow.GameTeams.Update(otherTeam);
            await ServiceUow.SaveChangesAsync();
            await ServiceRepository.RemoveAsync(gameEventId);
        }

        public async Task<IEnumerable<BLLAppDTO.GameTeam>> GetUpdateTeams(Guid gameEventId, IMapper mapper)
        {
            var gameEvent = await ServiceRepository.FirstOrDefaultAsync(gameEventId);

            var gameTeams = (await ServiceUow.GameTeams.GetAllTeamGamesWithGameIdAsync(gameEvent!.GameId))
                .ToList();

            var eventTeam = gameTeams.First(x => x.Id == gameEvent.GameTeamList!.GameTeamId);
            var otherTeam = gameTeams.First(x => x.Id != gameEvent.GameTeamList!.GameTeamId);

            if (gameEvent.EventType!.Name == "Goal" || gameEvent.EventType!.Name == "Penalty")
            {
               
            }else if (gameEvent.EventType!.Name == "Own goal")
            {
            }

            var gtMapper = new GameTeamMapper(mapper);
            var gTeams = new List<BLLAppDTO.GameTeam>();
            gTeams.Add(gtMapper.Map(eventTeam)!);
            gTeams.Add(gtMapper.Map(otherTeam)!);
            return gTeams;
        }
    }
}