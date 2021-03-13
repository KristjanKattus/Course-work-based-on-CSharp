using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Domain.Base;

namespace Contracts.DAL.Base.Repositories
{
    public interface IBaseRepositoryAsync<TEntity, TKey>: IBaseRepositoryCommon<TEntity, TKey>
        where TEntity : class, IDomainEntityId<TKey> // any more rules? maybe ID?
        where TKey : IEquatable<TKey>
    {
        // async
        Task<IEnumerable<TEntity>> GetAllAsync(bool noTracking = true);
        Task<TEntity?> FirstOrDefaultAsync(TKey id, bool noTracking = true);
        Task<bool> ExistsAsync(TKey id);
        Task<TEntity> RemoveAsync(TKey id);

    }
}