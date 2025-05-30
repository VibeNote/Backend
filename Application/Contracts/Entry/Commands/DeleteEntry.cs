using Dto.Entry;
using Mediator;

namespace Contracts.Entry.Commands;

public class DeleteEntry
{
    public record Command(Guid UserId, Guid EntryId) : IRequest;
}