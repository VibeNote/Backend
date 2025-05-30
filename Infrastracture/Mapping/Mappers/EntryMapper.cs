using Core.Entities;
using Dto.Analysis;
using Dto.Entry;
using Dto.Tag;
using Dto.TriggerWord;

namespace Mapper.Mappers;

public static class EntryMapper
{
    public static EntryFullInfoDto ToFullInfoDto(this Entry e)
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
    
    public static EntryShortInfoDto ToShortInfoDto(this Entry e)
    {
        List<AnalysisTagInfoDto> tags;
        tags = e.Analysis == null 
            ? [] 
            : e.Analysis.EmotionTags.Select(et => new AnalysisTagInfoDto(new TagDto(et.TagId, et.Tag.Value), et.Value)).ToList();

        return new EntryShortInfoDto(e.Id, e.Content, e.CreatedAt, e.UpdatedAt, tags);
    }
}