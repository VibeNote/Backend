using System.Security.Claims;
using Common.Exceptions.UnauthorizedExceptions;
using Microsoft.AspNetCore.Http;

namespace WebApp.Extensions;

public static class AuthificationExtensions
{
    public static Guid GetId(this ClaimsPrincipal claimsPrincipal)
    {
        var userIdString = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdString is null)
            throw TokenValidatorException.IsUnauthorized();

        return Guid.Parse(userIdString);
    }
    
    public static string GetToken(this HttpContext httpContext)
    {
        var token = httpContext.Request.Headers.Authorization.ToString()
            .Replace("Bearer ", "")
            .Trim();
        if (token is null)
            throw TokenValidatorException.IsUnauthorized();

        return token;
    }
    
}