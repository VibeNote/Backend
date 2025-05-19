using Common.Exceptions.BadRequestExceptions.Pagination;

namespace Common.Pagination;

public readonly struct Pagination
{
    public Pagination(int page, int pageSize)
    {
        if (page <= 0)
            throw PaginationException.InvalidPageNumber(page);

        Page = page;

        if (pageSize <= 0)
            throw PaginationException.InvalidPageSize(pageSize);

        PageSize = pageSize;
    }

    public int Page { get; }
    public int PageSize { get; }

    public void Deconstruct(out int page, out int pageSize)
    {
        page = Page;
        pageSize = PageSize;
    }
}