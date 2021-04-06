using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Contracts.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public class BaseRepository<TEntity, TDbContext> : BaseRepository<TEntity, Guid, TDbContext>, IBaseRepository<TEntity>
        where TEntity : class, IDomainEntityId
        where TDbContext: DbContext
    {
        public BaseRepository(TDbContext dbContext) : base(dbContext)
        {
        }
    }

    public class BaseRepository<TEntity, TKey, TDbContext> : IBaseRepository<TEntity, TKey>
        where TEntity : class, IDomainEntityId<TKey> 
        where TKey : IEquatable<TKey>
        where TDbContext: DbContext
    {
        protected readonly TDbContext RepoDbContext;
        protected readonly DbSet<TEntity> RepoDbSet;
        public BaseRepository(TDbContext dbContext)
        {
            RepoDbContext = dbContext;
            RepoDbSet = dbContext.Set<TEntity>();
        }

        private IQueryable<TEntity> InitializeQuery(TKey? userId = default, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();
            if (userId != null && typeof(TEntity).IsAssignableFrom(typeof(IDomainAppUserId<TKey>)))
            {
                // ReSharper disable once SuspiciousTypeConversion.Global
                query = query.Where(e => ((IDomainAppUserId<TKey>) e).AppUserId.Equals(userId));
            }
            if (noTracking)
            {
                query = query.AsNoTracking();
            }
            return query;
        }
        
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(TKey? userId = default, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);
            
            return await query.ToListAsync();
        }

        public virtual async Task<TEntity?> FirstOrDefaultAsync(TKey id, TKey? userId = default, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);
            
            return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public virtual TEntity Add(TEntity entity)
        {
            return RepoDbSet.Add(entity).Entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            return RepoDbSet.Update(entity).Entity;
        }

        public virtual TEntity Remove(TEntity entity, TKey? userId)
        {
            // var query = InitializeQuery(userId, false);

            if (userId != null && !((IDomainAppUserId<TKey>) entity).AppUserId.Equals(userId))
            {
                throw new AuthenticationException("Bad user id inside entity to be deleted.");
                //TODO: load entity from the db, check that userID inside entity is correct
            }
            
            return RepoDbSet.Remove(entity).Entity;        
        }

        public virtual async Task<TEntity> RemoveAsync(TKey id, TKey? userId = default)
        {
            var entity = await FirstOrDefaultAsync(id, userId);
            if (entity == null)
            {
                throw new NullReferenceException($"Entity with id {id} not found");}
            return Remove(entity!, userId);
        }

        public virtual async Task<bool> ExistsAsync(TKey id, TKey? userId = default)
        {
            return await RepoDbSet.AnyAsync(e => e.Id.Equals(id) && ((IDomainAppUserId<TKey>) e).AppUserId.Equals(userId));
        }
    }
}