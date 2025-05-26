using Dto.Common;
using Dto.User;
using Mediator;

namespace Contracts.Users.Commands;

public static class RegisterUser
{
    public record Command(RegisterUserDto RegisterUser) : IRequest<Response>;

    public record Response(TokenDto Token);
}