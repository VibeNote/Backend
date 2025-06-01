using System.Linq.Expressions;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Abstractions.Extensions;

public static class TokenFilterExtensions
{
    public static Task<Token?> FindByTokenAndUserIdAsync(this DbSet<Token> tokens, Guid userId, string value)
    {
        Expression<Func<Token, bool>> filterExpression =
            token => token.UserId == userId && token.Value == value;

        return tokens.FirstOrDefaultAsync(filterExpression);
    }
}