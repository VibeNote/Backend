using Microsoft.AspNetCore.Mvc;
using WebApp.Abstractions.Models.User;

namespace WebApp.Abstractions.Controllers;

public interface IUserApiController
{
    Task<IActionResult> Login(UserCredentialsModel credentialsModel, CancellationToken cancellationToken);
    Task<IActionResult> Register(RegisterUserModel registerModel, CancellationToken cancellationToken);
    Task<IActionResult> Logout(CancellationToken cancellationToken);
    Task<IActionResult> Update(UpdateUserModel updateModel, CancellationToken cancellationToken);
    Task<IActionResult> GetProfile(CancellationToken cancellationToken);
}