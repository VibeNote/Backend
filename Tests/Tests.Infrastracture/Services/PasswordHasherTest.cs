using Identity.Services;

namespace Infrastracture.Services;

[TestFixture]
[TestOf(typeof(PasswordHasher))]
public class PasswordHasherTest
{
    private const string Password1 = "ABOBA";
    private const string Password2 = "BOBAB";
    
    [Test]
    public void GenerateHashTest()
    {
        var hasher = new PasswordHasher();
        Assert.DoesNotThrow(() =>
        {
            hasher.GenerateHash(Password1);
        });
    }
    
    [Test]
    public void VerifyPasswordEqualTest()
    {
        var hasher = new PasswordHasher();
        Assert.DoesNotThrow(() =>
        {
            var hash1 = hasher.GenerateHash(Password1);

            var response = hasher.VerifyPassword(hash1, Password1);
            Assert.That(response, Is.EqualTo(true));
        });
    }
    
    [Test]
    public void VerifyPasswordNotEqualTest()
    {
        var hasher = new PasswordHasher();
        Assert.DoesNotThrow(() =>
        {
            var hash1 = hasher.GenerateHash(Password1);
            
            var response = hasher.VerifyPassword(hash1, Password2);
            Assert.That(response, Is.EqualTo(false));
        });
    }
}