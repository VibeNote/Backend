using Common.Exceptions.NotFoundExceptions.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Abstractions.Extensions;

public static class DbSetExtensions
{
    public static async Task<TEntity> GetByIdAsync<TEntity, TKey>(
        this DbSet<TEntity> dbSet,
        TKey key,
        CancellationToken cancellationToken = default)
        where TEntity : class
        where TKey : IEquatable<TKey>
    {
        var entity = await dbSet.FindAsync([key], cancellationToken);

        if (entity is null)
            throw EntityNotFoundException.For<TEntity, TKey>(key);

        return entity;
    }
    
    public static async Task<TEntity?> FindByIdAsync<TEntity, TKey>(
        this DbSet<TEntity> dbSet,
        TKey key,
        CancellationToken cancellationToken = default)
        where TEntity : class
        where TKey : IEquatable<TKey>
    {
        var entity = await dbSet.FindAsync([key], cancellationToken);

        return entity;
    }
}