using Common.Exceptions.ForbiddenExceptions.Abstractions;

namespace Common.Exceptions.ForbiddenExceptions;

public class NotEnoughAccessException : ForbiddenException
{
    protected NotEnoughAccessException(string message): base(message)
    {
    }

    public static NotEnoughAccessException UserCannotInteractWithEntry(Guid userId, Guid entryId)
        => new NotEnoughAccessException(
            $"Пользователь {userId} не может взаимодейстовать с записью {entryId}"
        );

    public static NotEnoughAccessException UserBlocked(Guid userId)
        => new NotEnoughAccessException(
            $"Пользователь {userId} заблокирован"
        );
}