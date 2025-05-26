using Dto.Common;
using Dto.User;
using Mediator;

namespace Contracts.Users.Queries;

public static class GetToken
{
    public record Query(Guid Id, string Token) : IRequest<Response>;

    public record Response(TokenDto TokenDto);
}