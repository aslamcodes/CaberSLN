using Caber.Contexts;
using Caber.Exceptions;
using Caber.Models;
using Caber.Models.DTOs;
using Caber.Repositories;
using Caber.Services;
using Microsoft.EntityFrameworkCore;

namespace CaberTests.ServicesTests
{
    public class PassengerServiceTests
    {
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

            passengerService = new PassengerService(new PassengerRepository(GetContext()), new UserRepository(GetContext()));
        }

        [Test]
        public async Task RegisterPassengerTest()
        {
            #region Arrange
            PassengerRegisterRequestDto passenger = new()
            {
                UserId = 1
            };
            #endregion

            #region Act
            var result = await passengerService.RegisterPassenger(passenger);
            #endregion

            #region Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.UserId, Is.EqualTo(1));
            #endregion
        }

        [Test]
        public async Task RegisterPassengerFailTest()
        {
            #region Arrange
            PassengerRegisterRequestDto passenger = new()
            {
                UserId = 12
            };
            #endregion


            #region Assert
            Assert.ThrowsAsync<UserNotFoundException>(async () => await passengerService.RegisterPassenger(passenger));
            #endregion
        }


        [TearDown]
        public void TearDown()
        {
            GetContext().Database.EnsureDeleted();
        }
    }
}
