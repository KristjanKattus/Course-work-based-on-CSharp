using System;
using System.Collections.Generic;

namespace DAL.App.EF
{
    public class AppUnitOfWork
    {
















        private Dictionary<Type, object> _repoCache = new Dictionary<Type, object>();

        private TRepository GetRepository<TRepository>(Func<TRepository> repoCreationMethod)
        where TRepository : class
        {
            if (_repoCache.TryGetValue(typeof(TRepository), out var repo))
            {
                return (TRepository) repo;
            }

            var repoInstance = repoCreationMethod();
            _repoCache.Add(typeof(TRepository), repoInstance);
            return repoInstance;
        }
    }
}