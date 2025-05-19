using Common.Exceptions.InternalServerExceptions.Abstractions;

namespace Common.Exceptions.InternalServerExceptions.Services;

public class ServiceProviderException : InternalServerException
{
    protected ServiceProviderException(string message): base(message)
    {
    }
    
    public static ServiceProviderException ServiceNotFound(Type tService)
    => new ServiceProviderException(
        $"Service for {tService.Name} was not found");
}