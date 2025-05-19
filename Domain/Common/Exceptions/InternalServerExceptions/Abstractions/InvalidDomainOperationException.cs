namespace Common.Exceptions.InternalServerExceptions.Abstractions;

public class InvalidDomainOperationException : InternalServerException
{
    protected InvalidDomainOperationException(string message): base(message)
    {
    }
}