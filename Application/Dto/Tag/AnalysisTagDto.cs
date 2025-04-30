using Dto.TriggerWord;

namespace Dto.Tag;

public record AnalysisTagDto(
    AnalysisTagInfoDto Info,
    IReadOnlyCollection<TriggerWordDto> TriggerWords);