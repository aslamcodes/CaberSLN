using Caber.Contexts;
using Caber.Exceptions;
using Caber.Models.DTOs;
using Caber.Repositories;
using Caber.Services;
using Caber.Services.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace CaberTests.ServicesTests
{
    public class AuthServiceTests
    {
        private ITokenService TokenService;
        private IAuthService AuthService;
        private CaberContext context;
        private PassengerService passengerService;
        private CaberContext GetContext()
        {
            return context;
        }

        private void SetContext(CaberContext value)
        {
            context = value;
        }

        [SetUp]
        public void Setup()
        {
            Mock<IConfigurationSection> configurationJWTSection = new();
            configurationJWTSection.Setup(x => x.Value).Returns("This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing");

            Mock<IConfigurationSection> congigTokenSection = new();
            congigTokenSection.Setup(x => x.GetSection("key")).Returns(configurationJWTSection.Object);

            Mock<IConfiguration> mockConfig = new();
            mockConfig.Setup(x => x.GetSection("TokenKey")).Returns(congigTokenSection.Object);

            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<CaberContext>()
               .UseSqlite(connection)
               .Options;

            SetContext(new CaberContext(options));
            GetContext().Database.EnsureCreated();


            TokenService = new TokenService(mockConfig.Object);
            AuthService = new AuthService(TokenService,
                                          new UserRepository(GetContext()),
                                          new UserRepository(GetContext()),
                                          new PassengerRepository(GetContext()));
        }

        [Test]
        public async Task AuthUserTest()
        {
            #region Arrange
            RegisterRequestDto registerDto = new()
            {
                Email = "johsa@gmail.com",
                FirstName = "joh",
                Password = "1234",
                Phone = "12",
                Address = "12",
                LastName = "sa"
            };

            LoginRequestDto loginRequest = new()
            {
                Email = "johsa@gmail.com",
                Password = "1234"
            };
            #endregion

            #region Act
            var registerResult = await AuthService.Register(registerDto);
            var loginResult = await AuthService.Login(loginRequest);
            #endregion

            #region Assert

            Assert.Multiple(() =>
            {

                Assert.That(loginResult, Is.Not.Null);
                Assert.That(registerResult, Is.Not.Null);
            });
            Assert.Multiple(() =>
            {
                Assert.That(registerResult.Token, Is.Not.Null);
                Assert.That(loginResult.Token, Is.Not.Null);
            });


            #endregion
        }

        [Test]
        public async Task AuthUseFailTest()
        {
            #region Arrange
            RegisterRequestDto registerDto = new()
            {
                Email = "johsa@gmail.com",
                FirstName = "joh",
                Password = "1234",
                Phone = "12",
                Address = "12",
                LastName = "sa"
            };

            #endregion

            #region Act
            var registerResult = await AuthService.Register(registerDto);
            #endregion

            #region Assert

            Assert.ThrowsAsync<CannotRegisterUserException>(async () => await AuthService.Register(registerDto));

            #endregion
        }


        [Test]
        public async Task AuthUserTestFail()
        {
            #region Arrange
            RegisterRequestDto registerDto = new()
            {
                Email = "johsa@gmail.com",
                FirstName = "joh",
                Password = "1234",
                Phone = "12",
                Address = "12",
                LastName = "sa"
            };

            LoginRequestDto loginRequest = new()
            {
                Email = "johsa@gmail.com",
                Password = "12314"
            };
            #endregion

            #region Act
            var registerResult = await AuthService.Register(registerDto);

            #endregion

            #region Assert


            Assert.ThrowsAsync<UnauthorizedUserException>(async () => await AuthService.Login(loginRequest));



            #endregion
        }


    }
}
