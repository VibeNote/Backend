using Common.Exceptions.NotFoundExceptions.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Abstractions.Extensions;

public static class DbSetExtensions
{
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
    
    public static TEntity? FindById<TEntity, TKey>(
        this DbSet<TEntity> dbSet,
        TKey key)
        where TEntity : class
        where TKey : IEquatable<TKey>
    {
        var entity = dbSet.Find([key]);

        return entity;
    }
}