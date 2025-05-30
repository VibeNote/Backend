using Microsoft.AspNetCore.Mvc;
using WebApp.Abstractions.Models.Entry;

namespace WebApp.Abstractions.Controllers;

public interface IEntryApiController
{
    Task<IActionResult> GetUserEntries(CancellationToken cancellationToken);
    Task<IActionResult> Save(InputEntryModel inputEntryModel, CancellationToken cancellationToken);
    Task<IActionResult> Update(Guid entryId, InputEntryModel inputEntryModel, CancellationToken cancellationToken);
    Task<IActionResult> Delete(Guid entryId, CancellationToken cancellationToken);
    Task<IActionResult> GetFullInfo(Guid entryId, CancellationToken cancellationToken);
}