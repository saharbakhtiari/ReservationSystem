using Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Common
{
    public interface IGenericRepository<TEntity, key>
        where TEntity : class, IEntity<key>
    {
        TEntity OwnerEntity { get; set; }
        /// <summary>
        /// Gets allitems of entity type without related entities in an eager loading fashion (i.e. all data items gets loaded in memory)
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Gets all items of enetity type in an eager loading fashion. Includes related entitites if specified in paramters.
        /// Example: await GetAllAsync(x => x.ChildEntityToInclude);
        /// </summary>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Gets all items of entity type in a lazy loading fashion, returning an IQueryable container. Data access will not execute unless items are accessed or converted to a list or something along those lines.
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAllAsQueryable(); // Not Async because we're returning a queryable collection

        /// <summary>
        /// Gets all items of entity type as queryable (lazy loading), along with related entities specified in includes parameter.
        /// Example usage:
        /// GetAllAsQuaeryable(x => x.ChildEntityToInclude);
        /// </summary>
        /// <param name="includes"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetAllAsQueryable(params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Gets one item specified by it's primary key value, ID. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asNoTracking">Set to true to stop tracking</param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(key id, bool asNoTracking = false);

        /// <summary>
        /// Get by Id and any include expressions. Example format:
        /// await GetByIdAsync(id = ... , includes = x => x.ChildEntityToInclue);
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asNoTracking">Set to true to stop tracking</param>
        /// <param name="includes">an expression to the child properties to include</param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(key id, bool asNoTracking = false, params Expression<Func<TEntity, object>>[] includes);

        Task CreateAsync(TEntity entity);

        Task CreateRangeAsync(IEnumerable<TEntity> entities);

        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Creates or Updates entity
        /// </summary>
        /// <param name="entity">Entity to upsert</param>
        /// <returns>Updated Entity</returns>
        Task<TEntity> UpsertAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task DeleteAllAsync();

        Task<TEntity> SaveAsync(CancellationToken cancellationToken);
        Task<TEntity> GetSingleByIdAsync(key id, bool asNoTracking = false, CancellationToken cancellationToken = default);
    }
}
