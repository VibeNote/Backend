using System.Net;
using System.Net.Http.Json;
using Common.Exceptions.BadRequestExceptions.Analysis;
using Common.Exceptions.BadRequestExceptions.Enums;
using Dto.Tag;
using Moq;
using Moq.Protected;

namespace Application.Analysis;

[TestFixture]
[TestOf(typeof(AnalysisService))]
public class AnalysisServiceTest
{

    [Test]
    public void GetContentTagsAsyncTest()
    {
        AnalysisService.RecommendResponse result = new AnalysisService.RecommendResponse()
        {
            Recommendation = "Hi"
        };
        var mockMessageHandler = new Mock<HttpMessageHandler>();
        
        var emotionClient = new HttpClient();
        var recommendationClient = GetMockedRecommendationClient(result);
        
        var service = new AnalysisService(emotionClient, recommendationClient);
        Assert.DoesNotThrowAsync(async() =>
        {
            var resultResponse = await service.GetResultAsync("", new List<AnalysedTagDto>());
            Assert.That(resultResponse, Is.Not.Null);
            Assert.That(resultResponse, Is.Not.Empty);
            Assert.That(resultResponse, Is.EqualTo(result.Recommendation));
        });
    }
    
    [Test]
    public void GetContentTagsAsyncResultNullTest()
    {
        AnalysisService.RecommendResponse? result = null;
        var mockMessageHandler = new Mock<HttpMessageHandler>();
        
        var emotionClient = new HttpClient();
        var recommendationClient = GetMockedRecommendationClient(result);
        
        var service = new AnalysisService(emotionClient, recommendationClient);
        Assert.ThrowsAsync<AnalysisServiceException>(async () =>
        {
            await service.GetResultAsync("", new List<AnalysedTagDto>());
        });
    }
    
    [Test]
    public void GetContentTagsAsyncResultEmptyTest()
    {
        var result = new AnalysisService.RecommendResponse()
        {
            Recommendation = ""
        };
        var mockMessageHandler = new Mock<HttpMessageHandler>();
        
        var emotionClient = new HttpClient();
        var recommendationClient = GetMockedRecommendationClient(result);
        
        var service = new AnalysisService(emotionClient, recommendationClient);
        Assert.ThrowsAsync<AnalysisServiceException>(async () =>
        {
            await service.GetResultAsync("", new List<AnalysedTagDto>());
        });
    }
    
    [Test]
    public void GetResultAsyncTest()
    {
        var fakeTags = new AnalysisService.TagsDictionary();
        fakeTags.Add("Joy", 0.5);
        fakeTags.Add("Anger", 0.4);
        fakeTags.Add("Sadness", 0.1);

        var emotionClient = GetMockedEmotionClient(fakeTags);
        var recommendationClient = new HttpClient();

        var service = new AnalysisService(emotionClient, recommendationClient);
        Assert.DoesNotThrowAsync(async () =>
        {
            var resultResponse = await service.GetContentTagsAsync("");
            Assert.That(resultResponse, Is.Not.Null);
            Assert.That(resultResponse, Is.Not.Empty);
            Assert.That(resultResponse, Has.Count.EqualTo(2));
        });
    }
    
    [Test]
    public void GetResultAsyncErrorResultTest()
    {
        var fakeTags = new AnalysisService.TagsDictionary();
        fakeTags.Add("Joy", 0.5);
        fakeTags.Add("Anger", 0.4);
        fakeTags.Add("Sadnes", 0.1);

        var emotionClient = GetMockedEmotionClient(fakeTags);
        var recommendationClient = new HttpClient();

        var service = new AnalysisService(emotionClient, recommendationClient);
        Assert.ThrowsAsync<EnumParsingException>(async () =>
        {
            await service.GetContentTagsAsync("");
        });
    }

    private static HttpClient GetMockedEmotionClient(AnalysisService.TagsDictionary fakeTags)
    {
        var mockMessageHandler = new Mock<HttpMessageHandler>();
        mockMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri!.AbsolutePath == "/analyze"),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(fakeTags)
            });

        return new HttpClient(mockMessageHandler.Object)
        {
            BaseAddress = new Uri("http://localhost")
        };
        
    }
    
    private static HttpClient GetMockedRecommendationClient(AnalysisService.RecommendResponse? response)
    {
        var handlerMock = new Mock<HttpMessageHandler>();

        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post && req.RequestUri.AbsolutePath.EndsWith("/recommend")),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(response)
            });

        return new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://localhost")
        };
    }
}