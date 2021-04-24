using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IGamePartTypeRepository : IBaseRepository<DALAppDTO.GamePartType>, IGamePartTypeRepositoryCustom<DALAppDTO.GamePartType>
    {
        
    }

    public interface IGamePartTypeRepositoryCustom<TEntity>
    {
    }
}