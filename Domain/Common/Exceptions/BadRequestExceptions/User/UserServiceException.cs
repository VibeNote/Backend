using Common.Exceptions.BadRequestExceptions.Abstractions;

namespace Common.Exceptions.BadRequestExceptions.User;

public class UserServiceException: BadRequestException
{
    protected UserServiceException(string message) : base(message)
    {
    }

    public static UserServiceException InvalidCredentials()
        => new UserServiceException("Неправильные данные для авторизации");
    
    public static UserServiceException UserNameIsNotUnique(string username)
        => new UserServiceException($"Логин {username} уже занят другим пользователем");
    
}