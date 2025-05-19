using Common.Pagination;
using Microsoft.EntityFrameworkCore;

namespace FloraPlanet.DataAccess.Abstractions.Extensions;

public static class PaginationExtensions
{
    public static async Task<PaginationResult<TData>> PaginateAsync<TData>(
        this IQueryable<TData> queryable,
        Pagination pagination,
        CancellationToken cancellationToken)
        where TData : class
    {
        var (page, pageSize) = pagination;

        var totalItemsCount = await queryable.CountAsync(cancellationToken);
        
        if (totalItemsCount is 0)
            return PaginationResult<TData>.Empty(pageSize);

        var totalPagesCount = (int)Math.Ceiling((decimal)totalItemsCount / pageSize);

        if (page > totalPagesCount)
            page = totalPagesCount;
        
        int skip = (page - 1) * pageSize;

        var data = await queryable
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginationResult<TData>(data, page, pageSize, totalPagesCount);
    }
}