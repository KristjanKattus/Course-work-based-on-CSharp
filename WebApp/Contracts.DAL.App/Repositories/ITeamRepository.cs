using System;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ITeamRepository : IBaseRepository<DALAppDTO.Team>, ITeamRepositoryCustom<DALAppDTO.Team>
    {
        
    }

    public interface ITeamRepositoryCustom<TEntity>
    {
        public Task<TEntity> FirstWithData(Guid id);
    }
}