using System.Net.Http.Json;
using Application.Abstractions.Analysis;
using Common.Exceptions.BadRequestExceptions.Analysis;
using Common.Extentions;
using Dto.Tag;

namespace Application.Analysis;

public class AnalysisService : IAnalysisService
{
    private readonly HttpClient _emotionClient;
    private readonly HttpClient _recomendationClient;
    private const string Recommend = "/recommend";
    private const string Analyze = "/analyze";

    public class TagsDictionary : Dictionary<string, double> { }
    public class RecommendResponse
    {
        public string Recommendation { get; set; } = string.Empty;
    }

    public AnalysisService(
        HttpClient emotionsContainerClient,
        HttpClient recommendationsContainerClient)
    {
        _emotionClient = emotionsContainerClient;
        _recomendationClient = recommendationsContainerClient;
    }

    public async Task<IReadOnlyCollection<AnalysedTagDto>> GetContentTagsAsync(string content,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var requestData = new { text = content };
        var response = await _emotionClient.PostAsJsonAsync(Analyze, requestData, cancellationToken);
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<TagsDictionary>(cancellationToken: cancellationToken);
        if (result == null)
        {
            throw AnalysisServiceException.NoTagsReturned();
        }
        return result
            .Select(td => new AnalysedTagDto(td.Key.FromEngName(), td.Value))
            .Where(t => t.Value >= 0.15d)
            .ToList();
    }

    public async Task<string> GetResultAsync(string content, IReadOnlyCollection<AnalysedTagDto> tags,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var dictionary = tags.ToDictionary(t => t.TagsEnum.ToEngName(), t => t.Value);
        var requestData = new { text = content , emotions = dictionary };
        var response = await _recomendationClient.PostAsJsonAsync(Recommend, requestData, cancellationToken);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<RecommendResponse>(cancellationToken);
        if (result == null || string.IsNullOrWhiteSpace(result.Recommendation))
        {
            throw AnalysisServiceException.NoResultReturned();
        }
        return result.Recommendation;
    }
}