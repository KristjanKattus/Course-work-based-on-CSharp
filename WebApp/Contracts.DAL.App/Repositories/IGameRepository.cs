using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IGameRepository : IBaseRepository<DALAppDTO.Game>, IGameRepositoryCustom<DALAppDTO.Game>
    {
        
    }

    public interface IGameRepositoryCustom<TEntity>
    {
    }
}