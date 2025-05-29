using Contracts.Analysis.Commands;
using Core.Entities;
using DataAccess.Abstractions;
using DataAccess.Abstractions.Extensions;
using Dto.Analysis;
using Dto.Entry;
using Dto.Tag;
using Dto.TriggerWord;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Handlers.Analysis;

using static AnalyseEntry;

public class AnalyseEntryHandler: IRequestHandler<Command, Response>
{
    private readonly IVibeNoteDatabaseContext _context;

    public AnalyseEntryHandler(IVibeNoteDatabaseContext context)
    {
        _context = context;
    }

    public async ValueTask<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var entry = await _context.Entries
            .Include(e => e.Analysis)
            .ThenInclude(a => a!.EmotionTags)
            .ThenInclude(et => et.Tag)
            .Include(e => e.Analysis)
            .ThenInclude(a => a!.EmotionTags)
            .ThenInclude(et => et.TriggerWords)
            .GetByIdAsync(request.EntryId, cancellationToken);

        if (entry.Analysis != null)
        {
            return new Response(new AnalysisDto(entry.Analysis.Id, entry.Content, entry.Analysis.Result,
                entry.Analysis.EmotionTags.Select(
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
                    .ToList()));
        }

        return new Response(new AnalysisDto(Guid.NewGuid(), entry.Content, "Chill...",
            new AnalysisTagDto[] { new AnalysisTagDto(new TagDto(new Guid("a8d5707f-2880-4d22-9561-7d13061b9930"), "Радость"), 10, new []{ new TriggerWordDto(Guid.NewGuid(),  "Бюджет")})}));
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