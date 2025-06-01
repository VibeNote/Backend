using Common.Exceptions.ForbiddenExceptions;
using Common.Exceptions.PreconditionFailedExceptions;
using Contracts.Entry.Commands;
using DataAccess.Abstractions;
using DataAccess.Abstractions.Extensions;
using Mapper.Mappers;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Handlers.Entries;

using static UpdateEntry;
public class UpdateEntryHandler: IRequestHandler<Command, Response>
{    
    private readonly IVibeNoteDatabaseContext _context;

    public UpdateEntryHandler(IVibeNoteDatabaseContext context)
    {
        _context = context;
    }
    public async ValueTask<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var entry = await _context.Entries
            .AsSplitQuery()
            .Include(e => e.Analysis)
            .GetByIdAsync(request.UpdateEntry.Id, cancellationToken);
        if (entry.UserId != request.UserId)
        {
            throw NotEnoughAccessException.UserCannotInteractWithEntry(request.UserId, request.UpdateEntry.Id);
        }
        
        if (entry.Analysis != null)
        {
            throw EntryLogicException.EntryCannotBeUpdatedAfterAnalysis(request.UpdateEntry.Id);
        }

        entry.Content = request.UpdateEntry.Content;
        _context.Entries.Update(entry);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(entry.ToFullInfoDto());
    }
}