using Contracts.Entry.Queries;
using DataAccess.Abstractions;
using DataAccess.Abstractions.Extensions;
using Mapper.Mappers;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Handlers.Entries;

using static GetUserEntries;

public class GetUserEntriesHandler : IRequestHandler<Query, Response>
{
    private readonly IVibeNoteDatabaseContext _context;

    public GetUserEntriesHandler(IVibeNoteDatabaseContext context)
    {
        _context = context;
    }

    public ValueTask<Response> Handle(Query request, CancellationToken cancellationToken)
    {
        var entries = _context.Entries
            .AsSplitQuery()
            .Include(e => e.Analysis)
            .ThenInclude(a => a!.EmotionTags)
            .ThenInclude(et => et.Tag)
            .FilterByUser(request.UserId)
            .AsEnumerable();

        return new ValueTask<Response>(new Response(
            entries
                .Select(e => e.ToShortInfoDto())
                .ToList()));
    }
}