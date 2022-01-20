using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories;

public class RepositoryBase<T> : IAsyncRepository<T>
    where T : EntityBase
{
    protected OrderContext DbContext { get; private set; }

    public RepositoryBase(OrderContext orderContext)
    {
        this.DbContext = orderContext ?? throw new ArgumentNullException(nameof(orderContext));
    }

    public async Task<T> AddAsync(T entity)
    {
        this.DbContext.Set<T>().Add(entity);
        await this.DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        this.DbContext.Set<T>().Remove(entity);
        await this.DbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await this.DbContext.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await this.DbContext.Set<T>()
           .Where(predicate)
           .ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeString = null, bool disableTracking = true)
    {
        IQueryable<T> query = this.DbContext.Set<T>();

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        if (!string.IsNullOrWhiteSpace(includeString))
        {
            query = query.Include(includeString);
        }

        if (orderBy is not null)
        {
            return await orderBy(query).ToListAsync();
        }

        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<Expression<Func<T, object>>>? includes = null, bool disableTracking = true)
    {
        IQueryable<T> query = this.DbContext.Set<T>();

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        if (includes is not null)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        if (orderBy is not null)
        {
            return await orderBy(query).ToListAsync();
        }

        return await query.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await this.DbContext.Set<T>().FindAsync(id);
    }

    public async Task UpdateAsync(T entity)
    {
        this.DbContext.Entry(entity).State = EntityState.Modified;
        await this.DbContext.SaveChangesAsync();
    }
}
