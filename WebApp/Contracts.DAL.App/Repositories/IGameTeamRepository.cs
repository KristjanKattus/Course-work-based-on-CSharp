using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IGameTeamRepository : IBaseRepository<DALAppDTO.GameTeam>, IGameTeamRepositoryCustom<DALAppDTO.GameTeam>
    {
        
    }

    public interface IGameTeamRepositoryCustom<TEntity>
    {
    }
}