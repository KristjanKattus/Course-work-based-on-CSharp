using System;
using System.Collections.Generic;
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
        
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(bool noTracking = true)
        {
            if (noTracking)
            {
                return await RepoDbSet.AsNoTracking().ToListAsync();
            }
            return await RepoDbSet.ToListAsync();
        }

        public virtual async Task<TEntity?> FirstOrDefaultAsync(TKey id, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();
            
            if (noTracking)
            {
                query = query.AsNoTracking();
            }
            
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

        public virtual TEntity Remove(TEntity entity)
        {
            return RepoDbSet.Remove(entity).Entity;        
        }

        public virtual async Task<TEntity> RemoveAsync(TKey id)
        {
            var entity = await FirstOrDefaultAsync(id);
            return Remove(entity);
        }

        public virtual async Task<bool> ExistsAsync(TKey id)
        {
            return await RepoDbSet.AnyAsync(e => e.Id.Equals(id));
        }
    }
}