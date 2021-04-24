using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ILeagueRepository : IBaseRepository<DALAppDTO.League>, ILeagueRepositoryCustom<DALAppDTO.League>
    {
        
    }

    public interface ILeagueRepositoryCustom<TEntity>
    {
    }
}