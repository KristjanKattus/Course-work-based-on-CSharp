using System;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IGameService : IBaseEntityService<BLLAppDTO.Game, DALAppDTO.Game>, IGameRepositoryCustom<BLLAppDTO.Game>
    {
        
    }

    public interface IGameRepositoryCustom<TEntity>
    {
        public Task<BLLAppDTO.Game> FirstOrDefaultAsync(Guid gameId);
    }
}