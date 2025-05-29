using Contracts.Analysis.Commands;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Abstractions.Controllers;
using WebApp.Extensions;

namespace WebApp.Controllers;

[ApiController]
[Route("/analysis/")]
public class AnalysisApiApiController : ControllerBase, IAnalysisApiController
{
    private readonly IMediator _mediator;

    public AnalysisApiApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    
    [HttpPost("{id}")]
    [Authorize]
    public async Task<IActionResult> AnalyseById([FromRoute] Guid entryId, CancellationToken cancellationToken)
    {
        var userId = User.GetId();

        var analysisResponse = await _mediator.Send(new AnalyseEntry.Command(userId, entryId), cancellationToken);

        return Ok(analysisResponse.Analysis);
    }
}