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


        public async Task<BLLAppDTO.GameTeam> FirstOrDefaultWithGameIdAsync(Guid id, bool homeTeam)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultWithGameIdAsync(id, homeTeam))!;
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

        public async Task RemoveWithGameIdAsync(Guid gameId)
        {
            ServiceRepository.Remove(await ServiceRepository.FirstOrDefaultWithGameIdAsync(gameId, true));
            await ServiceUow.SaveChangesAsync();
            ServiceRepository.Remove(await ServiceRepository.FirstOrDefaultWithGameIdAsync(gameId, false));
            await ServiceUow.SaveChangesAsync();

        }

        public async Task UpdateEntity(Guid gameEventId)
        {
            
            var gameEvent = await ServiceUow.GameEvents.FirstOrDefaultAsync(gameEventId);
            var team1 = await ServiceRepository.FirstOrDefaultWithGameIdAsync(gameEvent!.GameId, true);
            var team2 = await ServiceRepository.FirstOrDefaultWithGameIdAsync(gameEvent!.GameId, false);
            

            
            
            if (gameEvent!.EventType!.Name == "Goal" || gameEvent.EventType!.Name == "Penalty")
            {
                if (gameEvent.GameTeamList!.GameTeamId == team1!.Id)
                {
                    team1!.Scored++;
                    team2!.Conceded++;
                }
                else
                {
                    team2!.Scored++;
                    team1!.Conceded++;
                }
                
            }else if (gameEvent.EventType!.Name == "Own goal")
            {
                if (gameEvent.GameTeamList!.GameTeamId == team1!.Id)
                {
                    team2!.Scored++;
                    team1!.Conceded++;
                }
                else
                {
                    team1!.Scored++;
                    team2!.Conceded++;
                }
            }

            ServiceRepository.Update(team1);
            
            await ServiceUow.SaveChangesAsync();
            
            ServiceRepository.Update(team2);
            
            await ServiceUow.SaveChangesAsync();
        }
    }
}