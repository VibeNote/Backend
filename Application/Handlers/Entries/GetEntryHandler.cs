using Common.Exceptions.ForbiddenExceptions;
using Contracts.Entry.Queries;
using DataAccess.Abstractions;
using DataAccess.Abstractions.Extensions;
using Mapper.Mappers;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Handlers.Entries;

using static GetEntry;

public class GetEntryHandler: IRequestHandler<Query, Response>
{
    private readonly IVibeNoteDatabaseContext _context;

    public GetEntryHandler(IVibeNoteDatabaseContext context)
    {
        _context = context;
    }

    public async ValueTask<Response> Handle(Query request, CancellationToken cancellationToken)
    {
        var entry = await _context.Entries
            .AsSplitQuery()
            .Include(e => e.Analysis)
            .ThenInclude(a => a!.EmotionTags)
            .ThenInclude(et => et.Tag)
            .Include(e => e.Analysis)
            .ThenInclude(a => a!.EmotionTags)
            .ThenInclude(et => et.TriggerWords)
            .GetByIdAsync(request.EntryId, cancellationToken);
        
        

        if (entry.UserId != request.UserId)
        {
            throw NotEnoughAccessException.UserCannotInteractWithEntry(request.UserId, request.EntryId);
        }

        return new Response(entry.ToFullInfoDto());
    }
}