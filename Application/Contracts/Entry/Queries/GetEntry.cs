using Dto.Entry;
using Mediator;

namespace Contracts.Entry.Queries;

public static class GetEntry
{
    public record Query(Guid UserId, Guid EntryId) : IRequest<Response>;
    public record Response(EntryFullInfoDto Entry);
}