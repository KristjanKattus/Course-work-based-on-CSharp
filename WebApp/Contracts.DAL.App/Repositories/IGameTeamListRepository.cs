using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IGameTeamListRepository : IBaseRepository<DALAppDTO.GameTeamList>, IGameTeamListRepositoryCustom<DALAppDTO.GameTeamList>
    {
        
    }

    public interface IGameTeamListRepositoryCustom<TEntity>
    {
    }
}