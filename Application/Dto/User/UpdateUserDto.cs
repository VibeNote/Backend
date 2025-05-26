namespace Dto.User;

public record UpdateUserDto(
    Guid UserId,
    string Username);