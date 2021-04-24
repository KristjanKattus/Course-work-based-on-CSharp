using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ITeamPersonRepository : IBaseRepository<DALAppDTO.TeamPerson>, ITeamPersonRepositoryCustom<DALAppDTO.TeamPerson>
    {
        
    }

    public interface ITeamPersonRepositoryCustom<TEntity>
    {
    }
}