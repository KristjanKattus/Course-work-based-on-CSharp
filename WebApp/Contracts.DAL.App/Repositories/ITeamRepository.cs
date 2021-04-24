using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ITeamRepository : IBaseRepository<DALAppDTO.Team>, ITeamRepositoryCustom<DALAppDTO.Team>
    {
        
    }

    public interface ITeamRepositoryCustom<TEntity>
    {
    }
}