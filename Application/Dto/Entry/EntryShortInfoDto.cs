using Dto.Tag;

namespace Dto.Entry;

public record EntryShortInfoDto(
    Guid Id,
    string Content,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    IReadOnlyCollection<AnalysisTagInfoDto> Tags);