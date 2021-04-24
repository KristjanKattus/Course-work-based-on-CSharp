using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IStadiumRepository : IBaseRepository<DALAppDTO.Stadium>, IStadiumRepositoryCustom<DALAppDTO.Stadium>
    {
        
    }

    public interface IStadiumRepositoryCustom<TEntity>
    {
    }
}