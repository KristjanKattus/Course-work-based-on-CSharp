using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IGameRepository : IBaseRepository<DALAppDTO.Game>, IGameRepositoryCustom<DALAppDTO.Game>
    {
        
    }

    public interface IGameRepositoryCustom<TEntity>
    {
        public Task<IEnumerable<DALAppDTO.Game>> GetAllGamesWithLeagueIdAsync(Guid leagueId);
        
        public Task<TEntity> FirstOrDefaultAsyncCustom(Guid gameId);
    }
}