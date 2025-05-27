using Contracts.Entry.Queries;
using Core.Entities;
using DataAccess.Abstractions;
using DataAccess.Abstractions.Extensions;
using Dto.Entry;
using Dto.Tag;
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
            .FilterByUser(request.UserId);

        return new ValueTask<Response>(new Response(entries.AsEnumerable().Select(Mapper).ToList()));
    }
    
    private static EntryShortInfoDto Mapper(Entry e)
    {
        List<AnalysisTagInfoDto> tags;
        tags = e.Analysis == null 
            ? [] 
            : e.Analysis.EmotionTags.Select(et => new AnalysisTagInfoDto(new TagDto(et.TagId, et.Tag.Value), et.Value)).ToList();

        return new EntryShortInfoDto(e.Id, e.Content, e.CreatedAt, e.UpdatedAt, tags);
    }
}