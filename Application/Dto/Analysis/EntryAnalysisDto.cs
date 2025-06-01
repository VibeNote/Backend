using Dto.Tag;

namespace Dto.Analysis;

public record EntryAnalysisDto(
    Guid Id,
    string Result,
    IReadOnlyCollection<AnalysisTagDto> Tags);