using Identity.Services;

namespace Infrastracture.Services;

[TestFixture]
[TestOf(typeof(TokenGenerator))]
public class TokenGeneratorTest
{
    private const string Secret =
        "11cd89e19da28c241699f8ecf7398c6b2db98c9a219bd901bde45d70ca1fc0792bb28047806e19e87022b499d7d9cd1f16f973e0d7e064b8715328ddd23055dbfb384a95b520611f845d41c6a67084d4fe5ae29d0978c9cdf07e8e3acc549c5c36e4693c1e604afa6b95e3fe44d4a41e86a269fc8376e91871ff56ca953c7b74c1baa281e182fd2f82fa5000cde8f31f5603f729b3918372ab9da009e86415392dd667da1404c9dc12a1ec933a51ecf25c8cd00893782eedc43a1176b23441e15a642a61c90e755db4af1acbe1d92651de82fdaffac6547cb242a1c1de9068a351d58967541ee070a6e321d8e800ee34e170660734b9754646df9d4668849a82";

    private const string Issuer = "Issuer";
    [Test]
    public void GenerateTokenTest()
    {
        var generator = new TokenGenerator(Secret, Issuer);
        
        Assert.DoesNotThrow((() =>
        {
            generator.GenerateToken(Guid.NewGuid());
        }));
    }
}