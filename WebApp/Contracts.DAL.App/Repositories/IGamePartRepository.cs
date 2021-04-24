using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IGamePartRepository : IBaseRepository<DALAppDTO.GamePart>, IGamePartRepositoryCustom<DALAppDTO.GamePart>
    {
        
    }

    public interface IGamePartRepositoryCustom<TEntity>
    {
    }
}