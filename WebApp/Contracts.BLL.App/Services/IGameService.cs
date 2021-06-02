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
    public interface IGameService : IBaseEntityService<BLLAppDTO.Game, DALAppDTO.Game>, IGameRepositoryCustom<BLLAppDTO.Game>
    {
        
    }

    public interface IGameRepositoryCustom<TEntity>
    {
        public Task<BLLAppDTO.Game> FirstOrDefaultAsync(Guid gameId);
        

        public Task<IEnumerable<BLLAppDTO.LeagueGame>> GetAllLeagueGameAsync(Guid leagueId, IMapper mapper);
        
        public Task<BLLAppDTO.LeagueGame> GetLeagueGameAsync(Guid gameId, IMapper mapper);
    }
}