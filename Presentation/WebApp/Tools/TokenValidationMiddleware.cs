using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Common.Exceptions.UnauthorizedExceptions;
using Contracts.Users.Queries;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace WebApp.Tools;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IMediator _mediator;

    public TokenValidationMiddleware(RequestDelegate next, IMediator mediator)
    {
        _next = next;
        _mediator = mediator;
    }

    public async Task InvokeAsync(HttpContext context)
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
                
                
                await _mediator.Send(new GetToken.Query(userId, token));

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