using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ILeagueTeamRepository : IBaseRepository<DALAppDTO.LeagueTeam>, ILeagueTeamRepositoryCustom<DALAppDTO.LeagueTeam>
    {
        
    }

    public interface ILeagueTeamRepositoryCustom<TEntity>
    {
        public Task<IEnumerable<TEntity>> GetAllWithLeagueIdAsync(Guid leagueId);

        
    }
}