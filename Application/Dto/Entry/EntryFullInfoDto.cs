using Dto.Analysis;

namespace Dto.Entry;

public record EntryFullInfoDto(
    Guid Id,
    string Content,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    EntryAnalysisDto? Analysis);