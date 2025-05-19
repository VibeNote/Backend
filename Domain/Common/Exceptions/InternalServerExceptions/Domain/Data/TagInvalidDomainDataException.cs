using Common.Exceptions.InternalServerExceptions.Abstractions;

namespace Common.Exceptions.InternalServerExceptions.Domain.Data;

public class TagInvalidDomainDataException : InvalidDomainDataException
{
    protected TagInvalidDomainDataException(string message): base(message)
    {
    }

    public static TagInvalidDomainDataException InvalidTagName(string? tagName)
        => new TagInvalidDomainDataException(
            $"Unexpected tag name {tagName}"
        );
}