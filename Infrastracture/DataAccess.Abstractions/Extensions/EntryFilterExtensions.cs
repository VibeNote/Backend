using System.Linq.Expressions;
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
}