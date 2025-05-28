using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Abstractions.User;
using Common.Exceptions.UnauthorizedExceptions;
using Microsoft.AspNetCore.Http;

namespace WebApp.Tools;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IUserService _userService;

    public TokenValidationMiddleware(RequestDelegate next, IUserService userService)
    {
        _next = next;
        _userService = userService;
    }

    public async Task InvokeAsync(HttpContext context, CancellationToken cancellationToken)
    {
        var authHeader = context.Request.Headers.Authorization.ToString();

        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
        {
            var token = authHeader["Bearer ".Length..].Trim();

            var handler = new JwtSecurityTokenHandler();

            try
            {
                var jwtToken = handler.ReadJwtToken(token);
                var userIdString = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userIdString == null)
                {
                    throw TokenValidatorException.IsUnauthorized();
                }

                var userId = Guid.Parse(userIdString);
                
                
                await _userService.GetToken(userId, token, cancellationToken);

                await _next(context);
            }
            catch (Exception ex)
            {
                if (ex is TokenValidatorException)
                {
                    throw;
                }
                throw TokenValidatorException.IsUnauthorized();
            }
        }
        else
        {
            await _next(context);
        }
    }
}