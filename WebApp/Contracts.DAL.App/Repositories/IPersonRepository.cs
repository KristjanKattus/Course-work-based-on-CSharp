using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPersonRepository : IBaseRepository<DALAppDTO.Person>, IPersonRepositoryCustom<DALAppDTO.Person>
    {
        
    }

    public interface IPersonRepositoryCustom<TEntity>
    {
    }
}