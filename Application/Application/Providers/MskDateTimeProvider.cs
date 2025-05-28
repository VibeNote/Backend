using Application.Abstractions.Providers;

namespace Application.Providers;

public class MskDateTimeProvider : IDateTimeProvider
{
    private static readonly TimeZoneInfo RussianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");
    
    public DateTime Now => DateTime.SpecifyKind(TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, RussianTimeZone), DateTimeKind.Utc);

    public DateOnly DateNow => DateOnly.FromDateTime(Now);
    
    public DateTime FromUtc(DateTime dateTime) => DateTime.SpecifyKind(TimeZoneInfo.ConvertTimeFromUtc(dateTime, RussianTimeZone), DateTimeKind.Utc);
}