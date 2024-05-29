using Caber;
using Caber.Contexts;
using Caber.Exceptions;
using Caber.Models;
using Caber.Models.DTOs;
using Caber.Repositories;
using Caber.Services;
using Caber.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CaberTests.ServicesTests
{
    public class DriverServiceTests
    {
        private CaberContext context;
        private IDriverService driverService;
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
                .UseInMemoryDatabase("CaberDriverTests")
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
            var user2 = new User()
            {
                Email = "1232@gmail.com",
                FirstName = "John",
                Password = new byte[] { 1, 2, 3, 4 },
                PasswordHashKey = new byte[] { 1, 2, 3, 4 },
                Phone = "123123",
                Address = "123"
            };
            GetContext().Users.Add(user);
            GetContext().Users.Add(user2);
            GetContext().SaveChanges();
            #endregion

            driverService = new DriverService(new DriverRepository(GetContext()),
                                              new UserRepository(GetContext()),
                                              new CabRepository(GetContext()),
                                              new RideRepository(GetContext())
                                              );
        }

        [Test]
        public async Task RegisterDriverTest()
        {
            #region Arrange
            DriverRegisterRequestDto driver = new()
            {
                UserId = 1,
                LicenseExpiryDate = DateTime.Now,
                LicenseNumber = "123456"
            };
            #endregion

            #region Act
            var result = await driverService.RegisterDriver(driver);
            #endregion

            #region Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.UserId, Is.EqualTo(1));
            #endregion
        }


        [Test]
        public async Task RegisterDriverFailTest()
        {
            #region Arrange
            DriverRegisterRequestDto driver = new()
            {
                UserId = 12
            };
            #endregion

            #region Assert
            Assert.ThrowsAsync<UserNotFoundException>(async () => await driverService.RegisterDriver(driver));
            #endregion
        }

        [Test]
        public async Task GetDriverRideRatingsTest()
        {
            #region Arrange
            Driver driver = new()
            {
                UserId = 1,
                LicenseExpiryDate = DateTime.Now,
                LicenseNumber = "123456"
            };
            GetContext().Drivers.Add(driver);
            GetContext().SaveChanges();

            var cab = new Cab()
            {
                DriverId = 1,
                Location = "123",
                Color = "Red",
                Make = "Toyota",
                Model = "Corolla",
                RegistrationNumber = "123",
                Status = "Active"
            };

            GetContext().Cabs.Add(cab);
            GetContext().SaveChanges();

            var passenger = new Passenger()
            {
                UserId = 1
            };

            GetContext().Passengers.Add(passenger);
            GetContext().SaveChanges();

            var ride = new Ride()
            {
                PassengerId = 1,
                CabId = 1,
                PassengerComment = "Great",
                EndLocation = "123",
                StartLocation = "123"
            };

            var ride2 = new Ride()
            {
                PassengerId = 1,
                CabId = 1,
                PassengerComment = "Good Ride",
                EndLocation = "123",
                StartLocation = "123"
            };

            GetContext().Rides.Add(ride);
            GetContext().Rides.Add(ride2);
            GetContext().SaveChanges();

            #endregion

            #region Act
            var result = await driverService.GetDriverRideRatings(1);
            #endregion

            #region Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            #endregion
        }

        [Test]
        public async Task GetDriverRidesTest()
        {
            #region Arrange
            Driver driver = new()
            {
                UserId = 1,
                LicenseExpiryDate = DateTime.Now,
                LicenseNumber = "123456"
            };
            GetContext().Drivers.Add(driver);


            var cab = new Cab()
            {
                DriverId = 1,
                Location = "123",
                Color = "Red",
                Make = "Toyota",
                Model = "Corolla",
                RegistrationNumber = "123",
                Status = "Active"
            };

            GetContext().Cabs.Add(cab);


            var passenger = new Passenger()
            {
                UserId = 1
            };

            GetContext().Passengers.Add(passenger);


            var ride = new Ride()
            {
                PassengerId = 1,
                CabId = 1,
                PassengerComment = "Great",
                EndLocation = "123",
                StartLocation = "123"
            };

            var ride2 = new Ride()
            {
                PassengerId = 1,
                CabId = 1,
                PassengerComment = "Good Ride",
                EndLocation = "123",
                StartLocation = "123"
            };

            GetContext().Rides.Add(ride);
            GetContext().Rides.Add(ride2);
            GetContext().SaveChanges();

            #endregion

            #region Act
            var result = await driverService.GetRidesForDriver(driverId: 1);
            #endregion

            #region Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            #endregion
        }


        [TearDown]
        public void TearDown()
        {
            GetContext().Database.EnsureDeleted();
        }
    }
}
