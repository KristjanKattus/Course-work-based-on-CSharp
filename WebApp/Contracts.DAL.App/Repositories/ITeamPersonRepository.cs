using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ITeamPersonRepository : IBaseRepository<DALAppDTO.TeamPerson>, ITeamPersonRepositoryCustom<DALAppDTO.TeamPerson>
    {
        
    }

    public interface ITeamPersonRepositoryCustom<TEntity>
    {
        public Task<IEnumerable<TEntity>>GetAllWithTeamIdAsync(Guid teamId);
    }
}