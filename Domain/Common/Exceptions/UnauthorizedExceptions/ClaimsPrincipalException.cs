using Common.Exceptions.UnauthorizedExceptions.Abstractions;

namespace Common.Exceptions.UnauthorizedExceptions;

public class TokenCheckerException : UnauthorizedException
{
    protected TokenCheckerException(string message): base(message)
    {
    }

    public static TokenCheckerException IsUnauthorized()
        => new TokenCheckerException(
            "Current user is unauthorized"
        );
    
    public static TokenCheckerException InvalidToken(Guid userId, string token)
        => new TokenCheckerException(
            $"User with id {userId} has no token {token}"
        );
}