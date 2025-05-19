using Common.Exceptions.BadRequestExceptions.Pagination;

namespace Common.Pagination;

public record PaginationResult<TData>(
    IReadOnlyCollection<TData> Data,
    int Page,
    int PageSize,
    int TotalPagesCount)
    where TData : class
{
    public int PreviousPage
    {
        get
        {
            if (!HasPreviousPage)
                throw PaginationException.NoPreviousPage();

            return Page - 1;
        }
    }

    public int NextPage
    {
        get
        {
            if (!HasNextPage)
                throw PaginationException.NoNextPage();
            
            return Page + 1;
        }
    }

    public bool HasPreviousPage => Page - 1 != 0;

    public bool HasNextPage => Page != TotalPagesCount;

    public static PaginationResult<TData> Empty(int pageSize)
    {
        return new PaginationResult<TData>(Array.Empty<TData>(), 1, pageSize, 1);
    } 
}
