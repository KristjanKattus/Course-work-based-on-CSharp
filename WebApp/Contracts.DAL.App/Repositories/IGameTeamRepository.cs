using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IGameTeamRepository : IBaseRepository<DALAppDTO.GameTeam>, IGameTeamRepositoryCustom<DALAppDTO.GameTeam>
    {
        
    }

    public interface IGameTeamRepositoryCustom<TEntity>
    {
        public Task<IEnumerable<TEntity>> GetAllTeamGamesAsync(Guid teamId);

        public Task<IEnumerable<TEntity>> GetAllTeamGamesWithGameIdAsync(Guid gameId,  bool noTracking = true);
        
        public Task<TEntity>FirstOrDefaultWithGameIdAsync(Guid id, bool homeTeam);
        

    }
}