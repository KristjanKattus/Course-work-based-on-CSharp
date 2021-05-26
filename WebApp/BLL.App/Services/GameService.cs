using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
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

        public async Task<BLLAppDTO.Game> FirstOrDefaultAsync(Guid gameId)
        {
            var game = Mapper.Map(await ServiceRepository.FirstOrDefaultAsync(gameId));

            var gameTeams = (await ServiceUow.GameTeams.GetAllTeamGamesWithGameIdAsync(gameId)).ToList();

            var homeTeam = gameTeams.FirstOrDefault(x => x.Hometeam);
            if (game!.GameEvents != null)
            {
                game.HomeTeamEvents = new List<BLLAppDTO.GameEvent>();
                game.AwayTeamEvents = new List<BLLAppDTO.GameEvent>();

                foreach (var gameEvent in game.GameEvents)
                {
                    if (gameEvent.GameTeamList!.GameTeamId == homeTeam!.Id)
                    {
                        game.HomeTeamEvents.Add(gameEvent);
                    }
                    else
                    {
                        game.AwayTeamEvents.Add(gameEvent);
                    }
                }
            }
            
            return game;
        }

        public async Task<IEnumerable<BLLAppDTO.LeagueGame>> GetAllLeagueGameAsync(Guid leagueId, IMapper mapper)
        {
            

            var games = (await ServiceRepository.GetAllGamesWithLeagueIdAsync(leagueId))
                .Select(x => Mapper.Map(x));

            var leagueGames = new List<BLLAppDTO.LeagueGame>();

            foreach (var game in games)
            {
                var leagueGame = await GetLeagueGameAsync(game!.Id, mapper);
                
                leagueGames.Add(leagueGame);
            }
            
            return leagueGames;
        }

        public async Task<BLLAppDTO.LeagueGame> GetLeagueGameAsync(Guid gameId, IMapper mapper)
        {
            var gameTeamMapper = new GameTeamMapper(mapper);
            
            var gameTeams = (await ServiceUow.GameTeams.GetAllTeamGamesWithGameIdAsync(gameId)).ToList();

            var leagueGame = new BLLAppDTO.LeagueGame
            {
                Game = Mapper.Map(await ServiceRepository.FirstOrDefaultAsync(gameId)),
                
                HomeTeam = gameTeamMapper.Map(gameTeams.FirstOrDefault(x => x.Hometeam)),
                AwayTeam = gameTeamMapper.Map(gameTeams.FirstOrDefault(x => x.Hometeam == false))
            };

            return leagueGame;
        }
    }
}