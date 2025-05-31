using System.Linq.Expressions;
using Common.Enums;
using Common.Exceptions.BadRequestExceptions.Enums;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Abstractions.Extensions;

public static class TagFilterExtensions
{
    public static async Task<Tag> GetByTag(this IQueryable<Tag> tags, TagsEnum tagsEnum)
    {
        Expression<Func<Tag, bool>> filterExpression =
            tag => tag.EnumValue == tagsEnum;

        var tag = await tags.FirstOrDefaultAsync(filterExpression);
        if (tag == null)
        {
            throw EnumParsingException.CannotParseEnum(tagsEnum);
        }

        return tag;
    }
    
    public static Task<Dictionary<TagsEnum, Tag>> ToEnumDictionary(this IQueryable<Tag> tags, IReadOnlyCollection<TagsEnum> tagEnums)
    {
        Expression<Func<Tag, bool>> filterExpression =
            tag => tagEnums.Contains(tag.EnumValue);

        return Task.FromResult(tags
            .Where(filterExpression)
            .AsEnumerable()
            .ToDictionary(t => t.EnumValue, t => t));
    }
}