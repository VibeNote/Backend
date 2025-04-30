using System;

namespace Dto.User;

public record UserInfoDto(
    Guid UserId,
    string Email,
    string Username);