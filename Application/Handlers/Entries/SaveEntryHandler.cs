using Application.Abstractions.Providers;
using Contracts.Entry.Commands;
using Core.Entities;
using DataAccess.Abstractions;
using Dto.Entry;
using Mediator;

namespace Handlers.Entries;

using static SaveEntry;

public class SaveEntryHandler: IRequestHandler<Command, Response>
{
    private readonly IVibeNoteDatabaseContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public SaveEntryHandler(IVibeNoteDatabaseContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }

    public async ValueTask<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var entry = new Entry(
            Guid.NewGuid(), 
            request.UserId, 
            request.InputEntry.Content, 
            DateTime.UtcNow,
            DateTime.UtcNow);

        await _context.Entries.AddAsync(entry, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(new EntryFullInfoDto(entry.Id, entry.Content, _dateTimeProvider.FromUtc(entry.CreatedAt), _dateTimeProvider.FromUtc(entry.UpdatedAt), null));
    }
}