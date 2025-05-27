using Application.Abstractions.User;
using Contracts.Users.Commands;
using Mediator;

namespace Handlers.Users;

using static RegisterUser;

public class RegisterUserHandler : IRequestHandler<Command, Response>
{
    private readonly IUserService _userService;

    public RegisterUserHandler(
        IUserService userService)
    {
        _userService = userService;
    }

    public async ValueTask<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var token = await _userService.Register(request.RegisterUser, cancellationToken);

        return new Response(token);
    }
}