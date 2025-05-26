using Common.Exceptions.UnauthorizedExceptions;
using Contracts.Users.Queries;
using DataAccess.Abstractions;
using DataAccess.Abstractions.Extensions;
using Dto.Common;
using Mediator;

namespace Handlers.Users;

using static GetToken;

public class GetTokenHandler: IRequestHandler<Query, Response>
{
    private readonly IVibeNoteDatabaseContext _context;

    public GetTokenHandler(IVibeNoteDatabaseContext context)
    {
        _context = context;
    }

    public async ValueTask<Response> Handle(Query request, CancellationToken cancellationToken)
    {
        var userId = request.Id;
        var token = request.Token;
        var user = await _context.Users.FindByIdAsync(userId, cancellationToken: cancellationToken);
        if (user == null)
        {
            throw TokenValidatorException.IsUnauthorized();
        }

        var tokenEntity = await _context
            .Tokens
            .FindByTokenAndUserId(userId, token);

        if (tokenEntity == null)
        {
            throw TokenValidatorException.InvalidToken(userId, token);
        }

        return new Response(new TokenDto(tokenEntity.Value, user.UserName));
    }
}