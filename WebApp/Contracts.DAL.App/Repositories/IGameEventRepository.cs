using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IGameEventRepository : IBaseRepository<DALAppDTO.GameEvent>, IGameEventRepositoryCustom<DALAppDTO.GameEvent>
    {
        
    }

    public interface IGameEventRepositoryCustom<TEntity>
    {
    }
}