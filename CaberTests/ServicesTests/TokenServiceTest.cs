using Caber.Models;
using Caber.Services;
using Caber.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;

namespace CaberTests.ServicesTests
{
    public class TokenServiceTest
    {
        [Test]
        public void TokenGenerationTest()
        {
            // Setup
            Mock<IConfigurationSection> configurationJWTSection = new();
            configurationJWTSection.Setup(x => x.Value).Returns("This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing");

            Mock<IConfigurationSection> congigTokenSection = new();
            congigTokenSection.Setup(x => x.GetSection("key")).Returns(configurationJWTSection.Object);

            Mock<IConfiguration> mockConfig = new();
            mockConfig.Setup(x => x.GetSection("TokenKey")).Returns(congigTokenSection.Object);

            ITokenService service = new TokenService(mockConfig.Object);

            //Action
            var token = service.GenerateUserToken(new User { Email = "testitout@gmial.ocm", FirstName = "test", Id = 103 });

            //Assert
            Assert.That(token, Is.Not.Null);
        }
    }
}
