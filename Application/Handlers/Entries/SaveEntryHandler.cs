using Contracts.Entry.Commands;
using DataAccess.Abstractions;
using Mediator;

namespace Handlers.Entries;

using static SaveEntry;

public class SaveEntryHandler: IRequestHandler<Command, Response>
{
    private readonly IVibeNoteDatabaseContext _context;

    public SaveEntryHandler(IVibeNoteDatabaseContext context)
    {
        _context = context;
    }

    public ValueTask<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}