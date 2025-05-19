namespace Common.Exceptions.InternalServerExceptions.Abstractions;

public class InvalidDomainDataException : InternalServerException
{
    protected InvalidDomainDataException(string message): base(message)
    {
    }
}