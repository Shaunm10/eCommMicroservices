using Ordering.Domain.Common;
using System.Linq.Expressions;

namespace Ordering.Application.Contracts.Persistence;

public interface IAsyncRepository<T>
    where T : EntityBase
{
    /// <summary>
    /// Get's all the Entities in the repository
    /// </summary>
    /// <returns>Read-only list of entities.</returns>
    Task<IReadOnlyList<T>> GetAllAsync();

    /// <summary>
    /// Get's all the Entities in the repository matching a predicate
    /// </summary>
    /// <param name="predicate">Filter for the entities</param>
    /// <returns>Read-only list of entities.</returns>
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Get's all the Entities in the repository matching a predicate
    /// </summary>
    /// <param name="predicate">Filter for the entities.</param>
    /// <param name="orderBy">How the result set should be ordered</param>
    /// <param name="includeString">Additional entities to include in result set.</param>
    /// <param name="disableTracking">If persistence tracking should be turned off</param>
    /// <returns>Read-only list of entities.</returns>
    Task<IReadOnlyList<T>> GetAsync(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>,
        IOrderedQueryable<T>>? orderBy = null,
        string? includeString = null,
        bool disableTracking = true);

    Task<IReadOnlyList<T>> GetAsync(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>,
        IOrderedQueryable<T>>? orderBy = null,
        List<Expression<Func<T, object>>>? includes = null,
        bool disableTracking = true);

    /// <summary>
    /// Gets a entity give it's Id.
    /// </summary>
    /// <param name="id">The primary identifier of the entity</param>
    /// <returns></returns>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Add an Entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Updates an Entities properties based on it's Id
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task UpdateAsync(T entity);

    /// <summary>
    /// Deletes an Entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task DeleteAsync(T entity);
}
