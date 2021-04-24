﻿using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ILeagueTeamRepository : IBaseRepository<DALAppDTO.LeagueTeam>, ILeagueTeamRepositoryCustom<DALAppDTO.LeagueTeam>
    {
        
    }

    public interface ILeagueTeamRepositoryCustom<TEntity>
    {
    }
}