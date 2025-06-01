using Contracts.Users.Commands;
using DataAccess.Abstractions;
using DataAccess.Abstractions.Extensions;
using Dto.User;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Handlers.Users;

using static UpdateProfile;

public class UpdateProfileHandler: IRequestHandler<Command, Response>
{
    private readonly IVibeNoteDatabaseContext _context;

    public UpdateProfileHandler(
        IVibeNoteDatabaseContext context)
    {
        _context = context;
    }

    public async ValueTask<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .AsSplitQuery()
            .GetByIdAsync(request.UpdateUser.UserId, cancellationToken);

        user.UserName = request.UpdateUser.Username;

        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(new UserInfoDto(user.Id, user.Login, user.UserName));
    }
}