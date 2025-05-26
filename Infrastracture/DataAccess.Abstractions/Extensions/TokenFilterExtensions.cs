using System.Linq.Expressions;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Abstractions.Extensions;

public static class TokenFilterExtensions
{
    public static IQueryable<Token> FilterByUser(this IQueryable<Token> tokens, Guid userId)
    {
        Expression<Func<Token, bool>> filterExpression =
            entry => entry.UserId == userId;

        return tokens.Where(filterExpression);
    }
    
    public static Task<Token?> FindByTokenAndUserId(this DbSet<Token> tokens, Guid userId, string value)
    {
        Expression<Func<Token, bool>> filterExpression =
            token => token.UserId == userId && token.Value == value;

        return tokens.FirstOrDefaultAsync(filterExpression);
    }
}