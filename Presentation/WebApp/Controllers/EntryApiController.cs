using Contracts.Entry.Commands;
using Contracts.Entry.Queries;
using Dto.Entry;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Abstractions.Controllers;
using WebApp.Abstractions.Models.Entry;
using WebApp.Extensions;

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
    [Authorize]
    public async Task<IActionResult> GetUserEntries(CancellationToken cancellationToken)
    {
        var userId = User.GetId();
        var entriesResponse = await _mediator.Send(new GetUserEntries.Query(userId), cancellationToken);

        return Ok(entriesResponse.Entries);
    }

    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Save(SaveEntryModel saveEntryModel, CancellationToken cancellationToken)
    {
        var userId = User.GetId();
        
        
        var saveResponse = await _mediator.Send(new SaveEntry.Command(userId, new InputEntryDto(saveEntryModel.Content)), cancellationToken);

        return Ok(saveResponse.Entry);
    }

    
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetFullInfo(Guid entryId, CancellationToken cancellationToken)
    {
        var userId = User.GetId();

        var entryResponse = await _mediator.Send(new GetEntry.Query(userId, entryId), cancellationToken);
        return Ok(entryResponse.Entry);
    }
}