using Dto.Entry;
using Mediator;

namespace Contracts.Entry.Commands;

public class UpdateEntry
{
    public record Command(Guid UserId, UpdateEntryDto UpdateEntry) : IRequest<Response>;
    public record Response(EntryFullInfoDto Entry);
}