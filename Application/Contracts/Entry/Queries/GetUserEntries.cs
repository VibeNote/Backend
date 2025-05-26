using Dto.Entry;
using Mediator;

namespace Contracts.Entry.Queries;

public static class GetUserEntries
{
    public record Query(Guid UserId) : IRequest<Response>;
    public record Response(IReadOnlyCollection<EntryShortInfoDto> Entries);
}