using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IClubRepository : IBaseRepository<DALAppDTO.Club>, IClubRepositoryCustom<DALAppDTO.Club>

    {

    }

    public interface IClubRepositoryCustom<TEntity>
    {
    }
}