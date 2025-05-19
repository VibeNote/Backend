using System.Linq.Expressions;
using Common.Exceptions.NotFoundExceptions.Entity;
using Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Abstractions.Extensions;

public static class QueryableExtensions
{
    public static async Task<TEntity> GetByIdAsync<TEntity, TId>(
        this IQueryable<TEntity> queryable,
        TId id,
        CancellationToken cancellationToken = default)
        where TEntity : class, IEntity<TId>
        where TId : IEquatable<TId>
    {
        var entity = await queryable.FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

        if (entity is null)
            throw EntityNotFoundException.For<TEntity, TId>(id);

        return entity;
    }

    public static async Task<TData> GetAsync<TData>(
        this IQueryable<TData> queryable,
        Expression<Func<TData, bool>> expression,
        CancellationToken cancellationToken = default)
        where TData : class
    {
        var entity = await queryable.FirstOrDefaultAsync(expression, cancellationToken);

        if (entity is null)
            throw EntityNotFoundException.For(expression);

        return entity;
    }
}