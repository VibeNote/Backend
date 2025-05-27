using Common.Exceptions.ForbiddenExceptions;
using Contracts.Entry.Queries;
using Core.Entities;
using DataAccess.Abstractions;
using DataAccess.Abstractions.Extensions;
using Dto.Analysis;
using Dto.Entry;
using Dto.Tag;
using Dto.TriggerWord;
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

        return new Response(Mapper(entry));
    }
    
    private static EntryFullInfoDto Mapper(Entry e)
    {
        var analysis = e.Analysis == null 
            ? null
            : new EntryAnalysisDto(
                e.Analysis.Id, 
                e.Analysis.Result, 
                e.Analysis.EmotionTags.Select(
                    et => new AnalysisTagDto(
                        new TagDto(
                            et.TagId, 
                            et.Tag.Value), 
                        et.Value, 
                        et.TriggerWords.Select(tw => 
                            new TriggerWordDto(
                                tw.Id, 
                                tw.Word)
                        ).ToList()))
                    .ToList());

        return new EntryFullInfoDto(e.Id, e.Content, e.CreatedAt, e.UpdatedAt, analysis);
    }
}