using System;
using System.Collections.Generic;
using Contracts.Domain.Base;

namespace Contracts.DAL.Base.Repositories
{
    public interface IBaseRepositorySync<TEntity, TKey>: IBaseRepositoryCommon<TEntity, TKey>
        where TEntity : class, IDomainEntityId<TKey> // any more rules? maybe ID?
        where TKey : IEquatable<TKey>
    {
        // non-async methods
        TEntity FirstOrDefault(TKey id, bool noTracking = true);
        IEnumerable<TEntity> GetAll(bool noTracking = true);
        bool Exists(TKey id);
        TEntity Remove(TKey id);
    }
}