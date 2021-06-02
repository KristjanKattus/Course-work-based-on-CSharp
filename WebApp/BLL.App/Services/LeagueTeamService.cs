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
    public class LeagueTeamService: BaseEntityService<IAppUnitOfWork, ILeagueTeamRepository, BLLAppDTO.LeagueTeam, DALAppDTO.LeagueTeam>, ILeagueTeamService
    {
        public LeagueTeamService(IAppUnitOfWork serviceUow, ILeagueTeamRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new LeagueTeamMapper(mapper))
        {
        }
       
        public async Task<IEnumerable<BLLAppDTO.LeagueTeam>> GetAllWithLeagueIdAsync(Guid leagueId)
        {
            
            return (await ServiceRepository.GetAllWithLeagueIdAsync(leagueId)).Select(x => Mapper.Map(x))!;
        }

        public async Task<IEnumerable<BLLAppDTO.LeagueTableTeam>> GetAllLeagueTeamsDataAsync(Guid leagueId)
        {
            var leagueTableTeams = new List<BLLAppDTO.LeagueTableTeam>();
            var leagueTeams = await GetAllWithLeagueIdAsync(leagueId);

            foreach (var leagueTeam in leagueTeams)
            {
                var gameTeams = (await ServiceUow.GameTeams.GetAllTeamGamesAsync(leagueTeam.TeamId)).ToList();
                
                
                var gamesPlayed = gameTeams.Count;
                var points = 0;
                var gamesWon = 0;
                var gamesLost = 0;
                int gamesTied = 0;
                var goalsScored = 0;
                var goalsConceded = 0;
                foreach (var gameTeam in gameTeams)
                {
                    points += gameTeam.Points;
                    gamesWon += gameTeam.Points == 3 ? 1 : 0;
                    gamesLost += gameTeam.Points == 0 ? 1 : 0;
                    gamesTied += gameTeam.Points == 1 ? 1 : 0;
                    goalsScored += gameTeam.Scored;
                    goalsConceded += gameTeam.Conceded;
                }

                var leagueTableTeam = new BLLAppDTO.LeagueTableTeam
                {
                    LeagueTeamName = leagueTeam.Team!.Name,
                    Points = points,
                    GamesPlayed = gamesPlayed,
                    GamesWon = gamesWon,
                    GamesLost = gamesLost,
                    GamesTied = gamesTied,
                    GoalsScored = goalsScored,
                    GoalsConceded = goalsConceded
                };
                leagueTableTeams.Add(leagueTableTeam);
            }

            return leagueTableTeams.OrderByDescending(x => x.Points);
        }
        
    }
}