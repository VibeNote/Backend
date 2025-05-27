using Application.Abstractions.User;
using Contracts.Users.Commands;
using Mediator;

namespace Handlers.Users;

using static LoginUser;

public class LoginUserHandler: IRequestHandler<Command, Response>
{
    private readonly IUserService _userService;

    public LoginUserHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async ValueTask<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var credentials = request.Credentials;
        
        var token = await _userService.Login(credentials, cancellationToken);

        return new Response(token);
    }
}