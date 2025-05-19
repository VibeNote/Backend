using System.Linq.Expressions;
using Core.Entities;

namespace DataAccess.Abstractions.Extensions;

public static class TokenFilterExtensions
{
    public static IQueryable<Token> FilterByUser(this IQueryable<Token> tokens, Guid userId)
    {
        Expression<Func<Token, bool>> filterExpression =
            entry => entry.UserId == userId;

        return tokens.Where(filterExpression);
    }
}