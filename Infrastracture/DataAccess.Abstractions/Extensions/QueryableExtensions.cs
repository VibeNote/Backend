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
}