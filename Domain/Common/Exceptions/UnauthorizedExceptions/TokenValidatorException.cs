using Common.Exceptions.UnauthorizedExceptions.Abstractions;

namespace Common.Exceptions.UnauthorizedExceptions;

public class TokenValidatorException : UnauthorizedException
{
    protected TokenValidatorException(string message): base(message)
    {
    }

    public static TokenValidatorException IsUnauthorized()
        => new TokenValidatorException(
            "Данный пользователь не авторизован"
        );
    
    public static TokenValidatorException InvalidToken(Guid userId, string token)
        => new TokenValidatorException(
            $"Пользователь с идентификатором {userId} не имеет токена {token}"
        );
}