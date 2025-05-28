namespace Application.Abstractions.Providers;

public interface IDateTimeProvider
{
    DateTime Now { get; }
    DateOnly DateNow { get; }
    public DateTime FromUtc(DateTime dateTime);
}