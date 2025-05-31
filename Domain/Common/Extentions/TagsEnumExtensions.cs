using Common.Enums;
using Common.Exceptions.BadRequestExceptions.Enums;

namespace Common.Extentions;

public static class TagsEnumExtensions
{
    public static string ToRuName(this TagsEnum tagsEnum)
        => tagsEnum switch
        {
            TagsEnum.Joy => "Радость",
            TagsEnum.Calmness => "Спокойствие",
            TagsEnum.Anger => "Злость",
            TagsEnum.Sadness => "Печаль",
            TagsEnum.Anxiety => "Тревога",
            TagsEnum.Confusion => "Растерянность",
            _ => throw new ArgumentOutOfRangeException(nameof(tagsEnum), tagsEnum, null)
        };
    
    public static string ToEngName(this TagsEnum tagsEnum)
        => tagsEnum switch
        {
            TagsEnum.Joy => nameof(TagsEnum.Joy),
            TagsEnum.Calmness => nameof(TagsEnum.Calmness),
            TagsEnum.Anger => nameof(TagsEnum.Anger),
            TagsEnum.Sadness => nameof(TagsEnum.Sadness),
            TagsEnum.Anxiety => nameof(TagsEnum.Anxiety),
            TagsEnum.Confusion => nameof(TagsEnum.Confusion),
            _ => throw new ArgumentOutOfRangeException(nameof(tagsEnum), tagsEnum, null)
        };

    public static TagsEnum FromEngName(this string tagsEnumString)
        => tagsEnumString switch
        {
            nameof(TagsEnum.Joy) => TagsEnum.Joy,
            nameof(TagsEnum.Calmness) => TagsEnum.Calmness,
            nameof(TagsEnum.Anger) => TagsEnum.Anger,
            nameof(TagsEnum.Sadness) => TagsEnum.Sadness,
            nameof(TagsEnum.Anxiety) => TagsEnum.Anxiety,
            nameof(TagsEnum.Confusion) => TagsEnum.Confusion,
            _ => throw EnumParsingException.CannotParseEnum<TagsEnum>(tagsEnumString)
        };
}