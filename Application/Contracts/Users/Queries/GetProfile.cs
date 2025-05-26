using Dto.User;
using Mediator;

namespace Contracts.Users.Queries;

public static class GetProfile
{
    public record Query(Guid Id) : IRequest<Response>;

    public record Response(UserInfoDto User);
}