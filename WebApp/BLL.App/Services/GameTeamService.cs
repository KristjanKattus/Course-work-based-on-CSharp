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

        public async Task<IEnumerable<BLLAppDTO.GameTeam>> GetAllTeamGamesWithGameIdAsync(Guid gameId,  bool noTracking = false)
        {
            var games = (await ServiceRepository.GetAllTeamGamesWithGameIdAsync(gameId, noTracking)).ToList();
            var gameTeams = games;
            foreach (var game in gameTeams)
            {
                game.TeamName = game.Team!.Name;
            }
            return gameTeams.Select(x => Mapper.Map(x))!;
        }
        

        public async Task<BLLAppDTO.GameTeam> FirstOrDefaultWithGameIdAsync(Guid id, bool homeTeam)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultWithGameIdAsync(id, homeTeam))!;
        }

        public async Task RemoveGamesWithGameIdAsync(Guid id,  bool noTracking = true)
        {
            var teamsToBeRemoved = (await ServiceRepository.GetAllTeamGamesWithGameIdAsync(id, noTracking));
            
            var removeGamesWithGameId = teamsToBeRemoved.ToList();
            foreach (var team in removeGamesWithGameId)
            {
                ServiceUow.GameTeams.Remove(team);
                await ServiceUow.SaveChangesAsync();
            }
            
        }
        

        public async Task AddGoal(Guid id)
        {
            var gameTeam = await ServiceRepository.FirstOrDefaultAsync(id);

            gameTeam!.Scored++;

            ServiceRepository.Update(gameTeam);
            await ServiceUow.SaveChangesAsync();
        }

        public async Task ConcedeGoal(Guid id)
        {
            var gameTeam = await ServiceRepository.FirstOrDefaultAsync(id);

            gameTeam!.Scored--;

            ServiceUow.GameTeams.Update(gameTeam);
            ServiceRepository.Update(gameTeam);
            await ServiceUow.SaveChangesAsync();
        }

        public async Task UpdateEntity(Guid gameTeamId, Guid gameEventId)
        {
            var gameTeam = await ServiceRepository.FirstOrDefaultAsync(gameTeamId);
            var gameEvent = await ServiceUow.GameEvents.FirstOrDefaultAsync(gameEventId);
            
            if (gameEvent!.EventType!.Name == "Goal" || gameEvent.EventType!.Name == "Penalty")
            {
                if (gameEvent.GameTeamList!.GameTeamId == gameTeam!.Id)
                {
                    gameTeam!.Scored++;
                }
                else
                {
                    gameTeam!.Scored--;
                }
                
            }else if (gameEvent.EventType!.Name == "Own goal")
            {
                if (gameEvent.GameTeamList!.GameTeamId == gameTeam!.Id)
                {
                    gameTeam!.Scored--;
                }
                else
                {
                    gameTeam!.Scored++;
                }
            }

            ServiceRepository.Update(gameTeam!);
            
            await ServiceUow.SaveChangesAsync();
        }
    }
}