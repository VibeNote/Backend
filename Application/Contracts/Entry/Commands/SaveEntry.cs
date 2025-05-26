using Dto.Common;
using Dto.Entry;
using Mediator;

namespace Contracts.Entry.Commands;

public static class SaveEntry
{
    public record Command(Guid UserId, InputEntryDto InputEntry) : IRequest<Response>;
    public record Response(EntryFullInfoDto Entry);
}