using Common.Enums;
using Dto.TriggerWord;

namespace Dto.Tag;

public record AnalysedTagDto(
    TagsEnum TagsEnum,
    double Value);