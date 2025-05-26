namespace Dto.User;

public record UserInfoDto(
    Guid UserId,
    string Login,
    string Username);