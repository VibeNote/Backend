using Microsoft.AspNetCore.Mvc;
using WebApp.Abstractions.Models.Entry;

namespace WebApp.Abstractions.Controllers;

public interface IEntryApiController
{
    Task<IActionResult> GetUserEntries(CancellationToken cancellationToken);
    Task<IActionResult> Save(SaveEntryModel saveEntryModel, CancellationToken cancellationToken);
    Task<IActionResult> GetFullInfo(Guid entryId, CancellationToken cancellationToken);
}