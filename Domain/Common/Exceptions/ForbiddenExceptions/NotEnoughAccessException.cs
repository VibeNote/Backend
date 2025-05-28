using Common.Exceptions.ForbiddenExceptions.Abstractions;

namespace Common.Exceptions.ForbiddenExceptions;

public class NotEnoughAccessException : ForbiddenException
{
    protected NotEnoughAccessException(string message): base(message)
    {
    }

    public static NotEnoughAccessException UserCannotInteractWithEntry(Guid userId, Guid entryId)
        => new NotEnoughAccessException(
            $"User with id {userId} cannot interact with entry with id {entryId}"
        );

    public static NotEnoughAccessException UserBlocked(Guid userId)
        => new NotEnoughAccessException(
            $"User with id {userId} is blocked"
        );
}