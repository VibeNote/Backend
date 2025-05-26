using Microsoft.AspNetCore.Mvc;

namespace WebApp.Abstractions.Controllers;

public interface IAnalysisApiController
{
    Task<IActionResult> AnalyseById(Guid entryId, CancellationToken cancellationToken);
}