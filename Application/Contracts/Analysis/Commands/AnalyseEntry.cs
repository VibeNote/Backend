using Dto.Analysis;
using Mediator;

namespace Contracts.Analysis.Commands;

public static class AnalyseEntry
{
    public record Command(Guid UserId, Guid EntryId) : IRequest<Response>;
    public record Response(AnalysisDto Analysis);
}