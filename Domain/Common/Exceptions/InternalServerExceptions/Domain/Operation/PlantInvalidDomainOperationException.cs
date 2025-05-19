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
            $"Emotion tag with id {emotionTagId} already has trigger word with id {triggerWordId}"
        );
}