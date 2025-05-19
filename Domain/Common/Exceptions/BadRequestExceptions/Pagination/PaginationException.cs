using Common.Exceptions.BadRequestExceptions.Abstractions;

namespace Common.Exceptions.BadRequestExceptions.Pagination;

public sealed class PaginationException : BadRequestException
{
    private PaginationException(string message): base(message)
    {
    }

    public static PaginationException InvalidPageNumber(int pageNumber)
    {
        return new PaginationException(
            $"Invalid page number:{pageNumber}"
            );
    }

    public static PaginationException InvalidPageSize(int pageSize)
    {
        return new PaginationException(
            $"Invalid page size: {pageSize}");
    }

    public static PaginationException NoNextPage()
    {
        return new PaginationException(
            "There are no next page");
    }

    public static PaginationException NoPreviousPage()
    {
        return new PaginationException(
            "There are no previous page");
    }
}