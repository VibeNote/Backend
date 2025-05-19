using System.Linq.Expressions;
using Common.Exceptions.NotFoundExceptions.Abstractions;

namespace Common.Exceptions.NotFoundExceptions.Entity;

public class EntityNotFoundException : NotFoundException
{
    private EntityNotFoundException(string message): base(message)
    {
    }

    public static EntityNotFoundException For<TEntity, TId>(TId id)
        where TEntity : class
        where TId : IEquatable<TId>
    {
        var message = $"Entity with type {typeof(TEntity).Name} and id {id} was not found";
        return new EntityNotFoundException(
            message);
    }

    public static EntityNotFoundException For<TEntity>(Expression<Func<TEntity, bool>> expression)
        where TEntity : class
    {
        var message = $"Entity with type {typeof(TEntity).Name} was not found by expression {expression}";
        return new EntityNotFoundException(
            message);
    }

    public static EntityNotFoundException For<TEntity>()
        where TEntity : class
    {
        var message = $"Entity with type {typeof(TEntity).Name} was not found";
        return new EntityNotFoundException(
            message);
    }
}