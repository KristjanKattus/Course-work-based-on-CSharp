using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IStadiumAreaRepository : IBaseRepository<DALAppDTO.StadiumArea>, IStadiumAreaRepositoryCustom<DALAppDTO.StadiumArea>
    {
        
    }

    public interface IStadiumAreaRepositoryCustom<TEntity>
    {
    }
}