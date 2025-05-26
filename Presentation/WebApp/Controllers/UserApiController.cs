using System.Security.Claims;
using Contracts.Users.Commands;
using Contracts.Users.Queries;
using Dto.User;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Abstractions.Controllers;
using WebApp.Abstractions.Models.User;
using WebApp.Extensions;

namespace WebApp.Controllers;

[ApiController]
[Route("/users/")]
public class UserApiController : ControllerBase, IUserApiController
{
    private readonly IMediator _mediator;

    public UserApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserCredentialsModel credentialsModel, CancellationToken cancellationToken)
    {
        var tokenResponse = await _mediator.Send(new LoginUser.Command(
                new CredentialsDto(
                    credentialsModel.Login,
                    credentialsModel.Password)),
            cancellationToken);

        return Ok(tokenResponse.Token);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserModel registerModel, CancellationToken cancellationToken)
    {
        var tokenResponse = await _mediator.Send(new RegisterUser.Command(
                new RegisterUserDto(
                    registerModel.Username,
                    new CredentialsDto(
                        registerModel.Credentials.Login,
                        registerModel.Credentials.Password))),
            cancellationToken);

        return Ok(tokenResponse.Token);
    }
    
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        var userId = User.GetId();
        var token = HttpContext.GetToken();

        await _mediator.Send(new LogoutUser.Command(userId, token), cancellationToken);
        return Ok();
    }


    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update(UpdateUserModel updateModel, CancellationToken cancellationToken)
    {
        var userId = User.GetId();

        var updatedProfileResponse =
            await _mediator.Send(new UpdateProfile.Command(new UpdateUserDto(userId, updateModel.Username)),
                cancellationToken);
        return Ok(updatedProfileResponse.User);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
    {
        var userId = User.GetId();

        var profileResponse = await _mediator.Send(new GetProfile.Query(userId), cancellationToken);
        return Ok(profileResponse.User);
    }
}