using Dto.User;
using Mediator;

namespace Contracts.Users.Commands;

public static class UpdateProfile
{
    public record Command(UpdateUserDto UpdateUser) : IRequest<Response>;

    public record Response(UserInfoDto User);
}