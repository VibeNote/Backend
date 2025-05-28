using Mediator;
using Contracts.Users.Queries;
using DataAccess.Abstractions;
using DataAccess.Abstractions.Extensions;
using Dto.User;

namespace Handlers.Users;

using static GetProfile;

public class GetProfileHandler: IRequestHandler<Query, Response>
{
    private readonly IVibeNoteDatabaseContext _context;

    public GetProfileHandler(IVibeNoteDatabaseContext context)
    {
        _context = context;
    }

    public async ValueTask<Response> Handle(Query request, CancellationToken cancellationToken)
    {
        var userId = request.Id;
        
        var user = await _context
            .Users
            .GetByIdAsync(userId, cancellationToken);
        
        return new Response(new UserInfoDto(user.Id, user.Login, user.UserName));
    }
}