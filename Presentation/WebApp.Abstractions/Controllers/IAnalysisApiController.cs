using Microsoft.AspNetCore.Mvc;
using WebApp.Abstractions.Models.Entry;

namespace FloraPlanet.WebApp.Abstractions.Controllers.Api;

public interface IAnalysisApiController
{
    Task<IActionResult> AnalyseById(Guid entryId, CancellationToken cancellationToken);
}