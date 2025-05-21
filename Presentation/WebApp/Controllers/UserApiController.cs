using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Abstractions.Controllers;
using WebApp.Abstractions.Models.User;

namespace WebApp.Controllers;

[ApiController]
[Route("/users/[action]")]
public class UserApiController : ControllerBase, IUserApiController
{
    private readonly IMediator _mediator;

    public UserApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public Task<IActionResult> Login(UserCredentialsModel credentialsModel, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public Task<IActionResult> Register(RegisterUserModel registerModel, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    public Task<IActionResult> Update(UpdateUserModel updateModel, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public Task<IActionResult> GetProfile(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}