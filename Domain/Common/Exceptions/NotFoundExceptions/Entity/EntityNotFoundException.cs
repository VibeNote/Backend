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
        var message = $"Сущности с типом {typeof(TEntity).Name} и идентификатором {id} не было найдено";
        return new EntityNotFoundException(
            message);
    }

    public static EntityNotFoundException For<TEntity>(Expression<Func<TEntity, bool>> expression)
        where TEntity : class
    {
        var message = $"Сущности с типом {typeof(TEntity).Name} не было найдено по выражению {expression}";
        return new EntityNotFoundException(
            message);
    }

    public static EntityNotFoundException For<TEntity>()
        where TEntity : class
    {
        var message = $"Сущности с типом {typeof(TEntity).Name} не было найдено";
        return new EntityNotFoundException(
            message);
    }
}