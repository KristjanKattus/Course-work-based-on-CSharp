using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IGamePersonnelRepository : IBaseRepository<DALAppDTO.GamePersonnel>, IGamePersonnelRepositoryCustom<DALAppDTO.GamePersonnel>
    {
        
    }

    public interface IGamePersonnelRepositoryCustom<TEntity>
    {
    }
}