using Dto.Tag;

namespace Dto.Analysis;

public record AnalysisDto(
    Guid Id,
    string EntryText,
    string Result,
    IReadOnlyCollection<AnalysisTagDto> Tags);