using Domain.Common;
using Domain.UnitOfWork;
using Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class GenericRepository<TEntity, key> : IGenericRepository<TEntity, key>
        where TEntity : class, IEntity<key>
    {
        protected virtual ApplicationDbContext _dbContext => _dbContextProvider.GetDbContext();

        public TEntity OwnerEntity { get; set; }

        private readonly IDbContextProvider<ApplicationDbContext> _dbContextProvider;
        public GenericRepository(IDbContextProvider<ApplicationDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        public async Task CreateAsync(TEntity entity)
        {
            var entitytmp = entity as IEntity<Guid>;
            if (entitytmp != null && entitytmp.Id == Guid.Empty)
            {
                entitytmp.Id = Guid.NewGuid();
            }

            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

        }

        public async Task CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
        }
        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            // Took inspiration from here: https://dev.to/j_sakamoto/how-to-get-the-actual-table-name-from-dbset-in-entityframework-core-20-56k0

            var model = _dbContext.Model;
            var entityTypes = model.GetEntityTypes();
            var entityType = entityTypes.FirstOrDefault(t => t.ClrType == typeof(TEntity));

            if (entityType != null)
            {
                var tableNameAnnotation = entityType.GetAnnotation("Relational:TableName");
                if (tableNameAnnotation != null)
                {
                    var tableNameOfEntity = tableNameAnnotation.Value.ToString();
                    await _dbContext.Database.ExecuteSqlInterpolatedAsync($"TRUNCATE TABLE {tableNameOfEntity}"); // TRUNCATE is faster than DELETE because it's a DDL, and table will remain but all data would be deleted
                }
            }
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await GetAllAsQueryable().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            return await GetAllAsQueryable(includes).ToListAsync();
        }

        public IQueryable<TEntity> GetAllAsQueryable()
        {
            return _dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAllAsQueryable(params Expression<Func<TEntity, object>>[] includes)
        {
            if (includes == null || includes.Any() == false)
            {
                return GetAllAsQueryable();
            }
            else
            {
                return includes.Aggregate(GetAllAsQueryable(),
                    (current, include) => current.Include(include));
            }
        }

        public async Task<TEntity> GetSingleByIdAsync(key id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> dbSet = _dbContext.Set<TEntity>().Where(CreateEqualityExpressionForId(id));

            if (asNoTracking)
            {
                dbSet = dbSet.AsNoTracking();
            }

            return await dbSet.SingleAsync(CreateEqualityExpressionForId(id), cancellationToken);

        }

        public async Task<TEntity> GetByIdAsync(key id, bool asNoTracking = false)
        {
            IQueryable<TEntity> dbSet = _dbContext.Set<TEntity>().Where(CreateEqualityExpressionForId(id));

            if (asNoTracking)
            {
                dbSet = dbSet.AsNoTracking();
            }

            return await dbSet.FirstOrDefaultAsync(CreateEqualityExpressionForId(id));

        }


        public async Task<TEntity> GetByIdAsync(key id, bool asNoTracking = false, params Expression<Func<TEntity, object>>[] includes)
        {
            if (includes == null || includes.Any() == false)
            {
                return await GetByIdAsync(id, asNoTracking);
            }
            else
            {
                if (asNoTracking)
                {
                    return includes.Aggregate(_dbContext.Set<TEntity>().Where(CreateEqualityExpressionForId(id)).AsNoTracking(),
                        (current, include) => current.Include(include))
                        .FirstOrDefault();
                }
                else
                {
                    return includes.Aggregate(_dbContext.Set<TEntity>().Where(CreateEqualityExpressionForId(id)),
                        (current, include) => current.Include(include))
                        .FirstOrDefault();
                }
            }
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Left for reference.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>true if successful</returns>
        private async Task<bool> UpsertReturnBoolAsync(TEntity entity)
        {
            var existingEntity = await GetByIdAsync(entity.Id);

            if (existingEntity != null)
            {
                await UpdateAsync(entity);
            }
            else
            {
                await CreateAsync(entity);
            }

            try
            {
                int res = await _dbContext.SaveChangesAsync();
                return res > 0;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<TEntity> UpsertAsync(TEntity entity)
        {
            var existingEntity = await GetByIdAsync(entity.Id);

            if (existingEntity != null)
            {
                await UpdateAsync(entity);
            }
            else
            {
                await CreateAsync(entity);
            }

            try
            {
                int res = await _dbContext.SaveChangesAsync();
                return await GetByIdAsync(entity.Id);

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<TEntity> SaveAsync(CancellationToken cancellationToken)
        {
            var existingEntity = await GetByIdAsync(OwnerEntity.Id);

            if (existingEntity == null)
            {
                await CreateAsync(OwnerEntity);
            }
            try
            {
                int res = await _dbContext.SaveChangesAsync(cancellationToken);
                return await GetByIdAsync(OwnerEntity.Id);
            }
            catch
            {
                throw;
            }
        }

        protected virtual Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(key id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var leftExpression = Expression.PropertyOrField(lambdaParam, "Id");

            var idValue = Convert.ChangeType(id, typeof(key));

            Expression<Func<object>> closure = () => idValue;
            var rightExpression = Expression.Convert(closure.Body, leftExpression.Type);

            var lambdaBody = Expression.Equal(leftExpression, rightExpression);

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
    }
}
