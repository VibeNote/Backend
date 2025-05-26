using Common.Exceptions.UnauthorizedExceptions.Abstractions;

namespace Common.Exceptions.UnauthorizedExceptions;

public class TokenValidatorException : UnauthorizedException
{
    protected TokenValidatorException(string message): base(message)
    {
    }

    public static TokenValidatorException IsUnauthorized()
        => new TokenValidatorException(
            "Current user is unauthorized"
        );
    
    public static TokenValidatorException InvalidToken(Guid userId, string token)
        => new TokenValidatorException(
            $"User with id {userId} has no token {token}"
        );
}