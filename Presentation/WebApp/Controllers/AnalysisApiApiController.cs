using FloraPlanet.WebApp.Abstractions.Controllers.Api;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("/api/Images")]
public class AnalysisApiApiController : ControllerBase, IAnalysisApiController
{
    private readonly IMediator _mediator;

    public AnalysisApiApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public Task<IActionResult> AnalyseById(Guid entryId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}