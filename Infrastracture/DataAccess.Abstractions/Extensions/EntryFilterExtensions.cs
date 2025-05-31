using System.Linq.Expressions;
using Common.Enums;
using Core.Entities;

namespace DataAccess.Abstractions.Extensions;

public static class EntryFilterExtensions
{
    public static IQueryable<Entry> FilterByUser(this IQueryable<Entry> entries, Guid userId)
    {
        Expression<Func<Entry, bool>> filterExpression =
            entry => entry.UserId == userId;

        return entries.Where(filterExpression);
    }
    
    public static IQueryable<Entry> FilterByTag(this IQueryable<Entry> entries, TagsEnum tagsEnum)
    {
        Expression<Func<Entry, bool>> filterExpression =
            entry => entry.Analysis.EmotionTags.Any(et => et.Tag.EnumValue == tagsEnum);

        return entries.Where(filterExpression);
    }
}