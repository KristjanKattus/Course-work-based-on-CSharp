using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Contracts.DAL.Base.Mapper;
using Contracts.DAL.Base.Repositories;
using Contracts.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public class BaseRepository<TDalEntity, TDomainEntity, TDbContext> : BaseRepository<TDalEntity, TDomainEntity, Guid, TDbContext>,
        IBaseRepository<TDalEntity>
    
        
        where TDalEntity : class, IDomainEntityId
        where TDomainEntity : class, IDomainEntityId
        
        where TDbContext: DbContext
    {
        public BaseRepository(TDbContext dbContext, IBaseMapper<TDalEntity, TDomainEntity> mapper) : base(dbContext, mapper)
        {
        }
    }

    public class BaseRepository<TDalEntity, TDomainEntity, TKey, TDbContext> : IBaseRepository<TDalEntity, TKey>

        where TDalEntity : class, IDomainEntityId<TKey>
        where TDomainEntity : class, IDomainEntityId<TKey>
        
        where TKey : IEquatable<TKey>
        where TDbContext: DbContext
    {
        protected readonly TDbContext RepoDbContext;
        protected readonly DbSet<TDomainEntity> RepoDbSet;
        protected readonly IBaseMapper<TDalEntity, TDomainEntity> Mapper;

        private readonly Dictionary<TDalEntity, TDomainEntity> _entityCache = new();
        public BaseRepository(TDbContext dbContext, IBaseMapper<TDalEntity, TDomainEntity> mapper)
        {
            RepoDbContext = dbContext;
            Mapper = mapper;
            RepoDbSet = dbContext.Set<TDomainEntity>();
        }

        protected IQueryable<TDomainEntity> InitializeQuery(TKey? userId = default, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();
            
            // TODO: Validate the input entity also
            if (userId != null && typeof(TDomainEntity).IsAssignableFrom(typeof(IDomainAppUserId<TKey>)))
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
        
        public virtual async Task<IEnumerable<TDalEntity>> GetAllAsync(TKey? userId, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);
            var resQuery = query.Select(domainEntity => Mapper.Map(domainEntity));
            var res = await resQuery.ToListAsync();
            
            return (res);
        }

        public virtual async Task<TDalEntity?> FirstOrDefaultAsync(TKey id, TKey? userId = default, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);
            
            return await query.Select(d => Mapper.Map(d)).FirstOrDefaultAsync(e => e!.Id.Equals(id));
        }

        public virtual TDalEntity Add(TDalEntity entity)
        {

            var domainEntity = Mapper.Map(entity)!;
            var updatedDomainEntity = RepoDbSet.Add(domainEntity).Entity;
            var dalEntity = Mapper.Map(updatedDomainEntity)!;
            
            _entityCache.Add(entity, domainEntity);
            
            return dalEntity;
        }
    
        public TDalEntity GetUpdatedEntityAfterSaveChanges(TDalEntity entity)
        {
            var updatedEntity = _entityCache[entity]!;
            var dalEntity = Mapper.Map(updatedEntity)!;
            return dalEntity;
        }
        
        
        public virtual TDalEntity Update(TDalEntity entity)
        {
            return Mapper.Map(RepoDbSet.Update(Mapper.Map(entity)!).Entity)!;
        }

        public virtual TDalEntity Remove(TDalEntity entity, TKey? userId)
        {
            // var query = InitializeQuery(userId, false);

            if (userId != null && !userId.Equals(default) &&
                typeof(IDomainAppUserId<TKey>).IsAssignableFrom(typeof(TDomainEntity)) &&
                    !((IDomainAppUserId<TKey>) entity).AppUserId.Equals(userId))
            {
                throw new AuthenticationException("Bad user id inside entity to be deleted.");
                //TODO: load entity from the db, check that userID inside entity is correct
            }
            
            return Mapper.Map(RepoDbSet.Remove(Mapper.Map(entity)!).Entity)!;        
        }

        

        public virtual async Task<TDalEntity> RemoveAsync(TKey id, TKey? userId = default)
        {
            var entity = await FirstOrDefaultAsync(id, userId);
            if (entity == null)
            {
                throw new NullReferenceException($"Entity with id {id} not found");}
            return Remove(entity!, userId);
        }

        public virtual async Task<bool> ExistsAsync(TKey id, TKey? userId = default)
        {
            if (userId == null || userId.Equals(default))
                return await RepoDbSet.AnyAsync(e => e.Id.Equals(id));

            if (!typeof(IDomainAppUserId<TKey>).IsAssignableFrom(typeof(TDomainEntity)))
                throw new AuthenticationException(
                    $"Entity {typeof(TDomainEntity).Name} does not implement required interface:" +
                    $" {typeof(IDomainAppUserId<TKey>).Name} for AppUserId check");
            
            return await RepoDbSet.AnyAsync(e => e.Id.Equals(id) && ((IDomainAppUserId<TKey>) e).AppUserId.Equals(userId));
        }
    }
}