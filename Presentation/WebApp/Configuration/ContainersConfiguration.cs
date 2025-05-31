namespace WebApp.Configuration;

public class ContainersConfiguration
{
    public required string PostgresName { get; init; }
    public required string WebAppName { get; init; }
    public required string RecommendationsName { get; init; }
    public required string EmotionsName { get; init; }
    public required int RecommendationsPort { get; init; }
    public required int EmotionsPort { get; init; }
}