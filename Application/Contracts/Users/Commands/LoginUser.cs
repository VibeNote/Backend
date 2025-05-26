using Dto.Common;
using Dto.User;
using Mediator;

namespace Contracts.Users.Commands;

public static class LoginUser
{
    public record Command(CredentialsDto Credentials) : IRequest<Response>;
    public record Response(TokenDto Token);
}