using Common.Exceptions.NotFoundExceptions.Abstractions;

namespace Common.Exceptions.NotFoundExceptions.User;

public class UserNotFoundException: NotFoundException
{
    protected UserNotFoundException(string message) : base(message)
    {
    }
    
        
    public static UserNotFoundException InvalidLogin()
        => new UserNotFoundException("Пользователь не найден");
}