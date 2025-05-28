using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Abstractions.User;
using Common.Exceptions.UnauthorizedExceptions;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.Tools;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{  
    private readonly IUserService _userService;
    public AuthorizeAttribute(IUserService userService)
    {
        _userService = userService;
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;
        
        var authHeader = context.HttpContext.Request.Headers.Authorization.ToString();
        var token = authHeader["Bearer ".Length..].Trim();
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var userIdString = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        
        if (userIdString == null)
        {
            throw TokenValidatorException.IsUnauthorized();
        }

        var userId = Guid.Parse(userIdString);
        
        var isExists = _userService.IsExists(userId);
        if (isExists)
        {
            throw TokenValidatorException.IsUnauthorized();
        }
            
    }
}