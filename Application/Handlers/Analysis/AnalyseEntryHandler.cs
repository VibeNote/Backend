using Application.Abstractions.Analysis;
using Common.Exceptions.PreconditionFailedExceptions;
using Common.Extentions;
using Contracts.Analysis.Commands;
using Core.Entities;
using DataAccess.Abstractions;
using DataAccess.Abstractions.Extensions;
using Dto.Analysis;
using Dto.Tag;
using Dto.TriggerWord;
using Mediator;
using Microsoft.EntityFrameworkCore;
using static System.Math;

namespace Handlers.Analysis;

using static AnalyseEntry;

public class AnalyseEntryHandler : IRequestHandler<Command, Response>
{
    private readonly IVibeNoteDatabaseContext _context;
    private readonly IAnalysisService _analysisService;

    public AnalyseEntryHandler(IVibeNoteDatabaseContext context, IAnalysisService analysisService)
    {
        _context = context;
        _analysisService = analysisService;
    }

    public async ValueTask<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var entry = await _context.Entries
            .AsSplitQuery()
            .Include(e => e.Analysis)
            .GetByIdAsync(request.EntryId, cancellationToken);

        if (entry.Analysis != null)
        {
            throw EntryLogicException.EntryHasAlreadyAnalysis(entry.Id);
        }

        var tags = await _analysisService.GetContentTagsAsync(entry.Content, cancellationToken);
        var result = await _analysisService.GetResultAsync(entry.Content, tags, cancellationToken);
        var tagsDict = await _context.Tags.AsQueryable().ToEnumDictionary(tags.Select(t => t.TagsEnum).ToList());
        var sumValue = tags.Sum(t => (int)(t.Value * 100));
        var valuesDict = tags
            .ToDictionary(
                t => t.TagsEnum, 
                t => Min((int)Round(t.Value * 100 / sumValue * 100), 100));

        var analysisId = Guid.NewGuid();
        var analysis = new Core.Entities.Analysis(analysisId, request.EntryId, result, DateTime.UtcNow);
        var emotionTags = tags
            .Select(t =>
                new EmotionTag(
                    Guid.NewGuid(),
                    analysisId,
                    tagsDict[t.TagsEnum].Id,
                    valuesDict[t.TagsEnum]
                )
            );

        await _context.Analyses.AddAsync(analysis, cancellationToken);
        await _context.EmotionTags.AddRangeAsync(emotionTags, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(
            new AnalysisDto(
                analysisId,
                entry.Content,
                result,
                tags.Select(t =>
                    new AnalysisTagDto(
                        new TagDto(
                            tagsDict[t.TagsEnum].Id,
                            t.TagsEnum.ToRuName()
                        ),
                        valuesDict[t.TagsEnum],
                        new List<TriggerWordDto>()
                    )
                ).ToList()
            )
        );
    }
}