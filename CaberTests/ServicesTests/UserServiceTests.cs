using Caber.Contexts;
using Caber.Models;
using Caber.Models.DTOs;
using Caber.Repositories;
using Caber.Services;
using Caber.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CaberTests.ServicesTests
{
    public class UserServiceTests
    {
        private CaberContext context;

        private IUserService userService;
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
            var options = new DbContextOptionsBuilder<CaberContext>()
             .UseInMemoryDatabase("CaberPassengerTests")
         .Options;

            SetContext(new CaberContext(options));
            GetContext().Database.EnsureCreated();

            #region Seed
            var user = new User()
            {
                Email = "123@gmail.com",
                FirstName = "John",
                Password = new byte[] { 1, 2, 3, 4 },
                PasswordHashKey = new byte[] { 1, 2, 3, 4 },
                Phone = "123123",
                Address = "123"
            };
            GetContext().Users.Add(user);
            GetContext().SaveChanges();
            #endregion

            userService = new UserService(new UserRepository(GetContext()));
        }

        [Test]
        [TestCase(1, null, null, "Wick", null)]
        [TestCase(1, null, "John", "Cena", null)]
        [TestCase(1, "Not in nyc", "John", "Cena", null)]
        [TestCase(1, "Not in nyc", "John", "Cena", "1123123")]

        public async Task UpdateUserProfileTest(int uid,
                                                string? address,
                                                string? firstName,
                                                string? lastName,
                                                string? phone)
        {
            #region Arrange
            var request = new UserProfileUpdateRequestDto()
            {
                Address = address,
                Id = uid,
                FirstName = firstName,
                LastName = lastName,
                Phone = phone
            };
            #endregion

            #region Act 
            await userService.UpdateUserProfile(request);
            #endregion

            #region Assert

            var user = GetContext().Users.Find(1);

            Assert.Multiple(() =>
            {
                Assert.That(user.FirstName, Is.EqualTo(firstName ?? user.FirstName));
                Assert.That(user.LastName, Is.EqualTo(lastName ?? user.LastName));
                Assert.That(user.Address, Is.EqualTo(address ?? user.Address));
                Assert.That(user.Phone, Is.EqualTo(phone ?? user.Phone));
            });

            #endregion
        }
        [Test]
        public async Task GetUserProfileTest()
        {
            #region Arrange
            var userId = 1;
            #endregion

            #region Act 
            var user = await userService.GetUserProfile(userId);
            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.That(user.FirstName, Is.EqualTo("John"));
                Assert.That(user.LastName, Is.Null);
                Assert.That(user.Address, Is.EqualTo("123"));
                Assert.That(user.Phone, Is.EqualTo("123123"));
            });

            #endregion
        }

        [TearDown]
        public void TearDown()
        {
            GetContext().Database.EnsureDeleted();
        }
    }
}
