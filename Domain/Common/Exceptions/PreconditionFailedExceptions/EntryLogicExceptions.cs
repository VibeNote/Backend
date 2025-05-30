using Common.Exceptions.PreconditionFailedExceptions.Abstractions;

namespace Common.Exceptions.PreconditionFailedExceptions;

public class EntryLogicException: PreconditionFailedException
{
    protected EntryLogicException(string message) : base(message)
    {
    }

    public static EntryLogicException EntryHasAlreadyAnalysis(Guid entryId)
        => new EntryLogicException($"Нельзя второй раз проанализировать запись {entryId}");
    
    public static EntryLogicException EntryCannotBeUpdatedAfterAnalysis(Guid entryId)
        => new EntryLogicException($"Нельзя обновить текст проанализированной записи {entryId}");
}