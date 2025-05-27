using Application.Abstractions.User;
using Contracts.Users.Commands;
using Mediator;

namespace Handlers.Users;

using static LogoutUser;

public class LogoutUserHandler: IRequestHandler<Command>
{
    private readonly IUserService _userService;

    public LogoutUserHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async ValueTask<Unit> Handle(Command request, CancellationToken cancellationToken)
    {
        await _userService.Logout(request.UserId, request.Token, cancellationToken);

        return new Unit();
    }
}