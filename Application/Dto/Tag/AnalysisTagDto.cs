using Dto.TriggerWord;

namespace Dto.Tag;

public record AnalysisTagDto(
    TagDto Tag,
    int Value,
    IReadOnlyCollection<TriggerWordDto> TriggerWords);