using System.Net;

namespace Dto.Common;

public record ErrorDto(
    HttpStatusCode Code,
    string Message);