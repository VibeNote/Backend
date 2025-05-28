using Contracts.Analysis.Commands;
using DataAccess.Abstractions;
using Mediator;

namespace Handlers.Analysis;

using static AnalyseEntry;

public class AnalyseEntryHandler: IRequestHandler<Command, Response>
{
    private readonly IVibeNoteDatabaseContext _context;

    public AnalyseEntryHandler(IVibeNoteDatabaseContext context)
    {
        _context = context;
    }

    public ValueTask<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}