namespace Dto.User;

public record RegisterUserDto(
    string Username,
    CredentialsDto CredentialsDto);