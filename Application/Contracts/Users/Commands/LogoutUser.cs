using Mediator;

namespace Contracts.Users.Commands;

public static class LogoutUser
{
    public record Command(Guid UserId, string Token) : IRequest;
}