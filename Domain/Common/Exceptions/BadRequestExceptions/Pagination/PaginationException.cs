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
            $"Неправильный номер страницы:{pageNumber}"
            );
    }

    public static PaginationException InvalidPageSize(int pageSize)
    {
        return new PaginationException(
            $"Неподдерживаемый размер страницы: {pageSize}");
    }

    public static PaginationException NoNextPage()
    {
        return new PaginationException(
            "Нет следующей страницы");
    }

    public static PaginationException NoPreviousPage()
    {
        return new PaginationException(
            "Нет предыдущей страницы");
    }
}