using Application.Providers;

namespace Application.Providers;

[TestFixture]
[TestOf(typeof(MskDateTimeProvider))]
public class MskDateTimeProviderTest
{

    [Test]
    public void HandleNowTest()
    {
        var provider = new MskDateTimeProvider();
        Assert.DoesNotThrow(() =>
        {
            var time = provider.Now;
        });
    }
    
    [Test]
    public void HandleDateUtcNowTest()
    {
        var provider = new MskDateTimeProvider();
        Assert.DoesNotThrow(() =>
        {
            var time = provider.DateNow;
        });
    }
    
    [Test]
    public void HandleFromUtcTest()
    {
        var provider = new MskDateTimeProvider();
        var time = DateTime.Parse("23-09-2002");
        var timeUtc = DateTime.Parse("23-09-2002").ToUniversalTime();
        Assert.DoesNotThrow(() =>
        {
            var given = provider.FromUtc(timeUtc);
            Assert.That(given, Is.EqualTo(time));
        });
    }
}