using Dto.Tag;

namespace Dto.Analysis;

public class EntryAnalysisDto(
    Guid Id,
    string Result,
    IReadOnlyCollection<AnalysisTagDto> Tags);