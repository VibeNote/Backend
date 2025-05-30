using Common.Exceptions.InternalServerExceptions.Abstractions;

namespace Common.Exceptions.InternalServerExceptions.Domain.Operation;

public class EmotionTagInvalidDomainOperationException : InvalidDomainOperationException
{
    protected EmotionTagInvalidDomainOperationException(string message) : base(message)
    {
    }

    public static EmotionTagInvalidDomainOperationException EmotionTagCannotAddTriggerWord(
        Guid emotionTagId,
        Guid triggerWordId)
        => new EmotionTagInvalidDomainOperationException(
            $"Тег с {emotionTagId} уже имеет ключевое слово {triggerWordId}"
        );
}