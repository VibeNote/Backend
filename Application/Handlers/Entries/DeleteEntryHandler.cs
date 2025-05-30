using Common.Exceptions.ForbiddenExceptions;
using Contracts.Entry.Commands;
using DataAccess.Abstractions;
using DataAccess.Abstractions.Extensions;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Handlers.Entries;

using static DeleteEntry;

public class DeleteEntryHandler: IRequestHandler<Command>
{
    private readonly IVibeNoteDatabaseContext _context;

    public DeleteEntryHandler(IVibeNoteDatabaseContext context)
    {
        _context = context;
    }
    public async ValueTask<Unit> Handle(Command request, CancellationToken cancellationToken)
    {
        var entry = await _context.Entries
            .Include(e => e.Analysis)
            .ThenInclude(a => a!.EmotionTags)
            .GetByIdAsync(request.EntryId, cancellationToken);
        if (entry.UserId != request.UserId)
        {
            throw NotEnoughAccessException.UserCannotInteractWithEntry(request.UserId, request.EntryId);
        }

        if (entry.Analysis != null)
        {
            _context.EmotionTags.RemoveRange(entry.Analysis.EmotionTags);
            _context.Analyses.Remove(entry.Analysis);
        }

        _context.Entries.Remove(entry);
        await _context.SaveChangesAsync(cancellationToken);

        return new Unit();
    }
}