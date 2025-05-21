using Mediator;
using Microsoft.AspNetCore.Mvc;
using WebApp.Abstractions.Controllers;
using WebApp.Abstractions.Models.Entry;

namespace WebApp.Controllers;

[Route("/entries/")]
public class EntryApiController: ControllerBase, IEntryApiController
{    
    private readonly IMediator _mediator;

    public EntryApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public Task<IActionResult> GetUserEntries(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public Task<IActionResult> Save(SaveEntryModel saveEntryModel, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public Task<IActionResult> GetFullInfo(Guid entryId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}