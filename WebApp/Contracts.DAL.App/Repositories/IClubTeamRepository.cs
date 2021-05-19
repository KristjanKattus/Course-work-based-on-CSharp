using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IClubTeamRepository : IBaseRepository<DALAppDTO.ClubTeam>, IClubTeamRepositoryCustom<DALAppDTO.ClubTeam>
    {
        
    }

    public interface IClubTeamRepositoryCustom<TEntity>
    {
    }
}