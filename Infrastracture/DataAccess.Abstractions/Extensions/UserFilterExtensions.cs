using System.Linq.Expressions;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Abstractions.Extensions;

public static class UserFilterExtensions
{
    public static Task<User?> FindByLogin(this DbSet<User> tokens, string login)
    {
        Expression<Func<User, bool>> filterExpression =
            user => user.Login == login;

        return tokens.FirstOrDefaultAsync(filterExpression);
    }
}