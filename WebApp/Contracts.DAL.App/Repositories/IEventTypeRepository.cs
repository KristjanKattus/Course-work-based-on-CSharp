using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IEventTypeRepository : IBaseRepository<DALAppDTO.EventType>, IEventTypeRepositoryCustom<DALAppDTO.EventType>
    {
        
    }

    public interface IEventTypeRepositoryCustom<TEntity>
    {
    }
}