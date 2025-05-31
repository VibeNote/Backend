using Dto.Tag;

namespace Application.Abstractions.Analysis;

public interface IAnalysisService
{
    Task<IReadOnlyCollection<AnalysedTagDto>> GetContentTagsAsync(string content, CancellationToken cancellationToken = new CancellationToken());
    Task<string> GetResultAsync(string content, IReadOnlyCollection<AnalysedTagDto> tags, CancellationToken cancellationToken = new CancellationToken());
}