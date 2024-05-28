using Caber;
using Caber.Contexts;
using Caber.Controllers;
using Caber.Models;
using Caber.Services;
using Caber.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CaberTests.ServicesTests
{
    public class RideServiceTests
    {
        private CaberContext context;

        private IRideService RideService;
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
            .UseInMemoryDatabase("RideTest")
        .Options;

            SetContext(new CaberContext(options));
            GetContext().Database.EnsureCreated();

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

            RideService = new RideService(new RideRepository(GetContext()));
        }

        [Test]
        public async Task CancelRide()
        {
            #region Arrage
            var passenger = new Passenger()
            {
                UserId = 1
            };

            var cab = new Cab()
            {
                RegistrationNumber = "123",
                Model = "GTR",
                Make = "Nissan",
                DriverId = 1,
                Status = "Idle",
                SeatingCapacity = 3,
                Color = "Red",
                Location = "Lahore"
            };

            Driver driver = new()
            {
                UserId = 1,
                LicenseExpiryDate = DateTime.Now,
                LicenseNumber = "123456"
            };


            var ride = new Ride()
            {
                CabId = 1,
                PassengerId = 1,
                EndLocation = "Test",
                StartLocation = "Test",
                RideStatus = Caber.Models.Enums.RideStatusEnum.Accepted
            };

            GetContext().Passengers.Add(passenger);

            GetContext().Drivers.Add(driver);

            GetContext().Cabs.Add(cab);

            GetContext().Rides.Add(ride);

            GetContext().SaveChanges();

            #endregion

            #region Act
            var cancelRideRequest = new CancelRideRequestDto()
            {
                RideId = 1
            };

            var result = await RideService.CancelRide(cancelRideRequest);
            #endregion

            #region Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Status, Is.EqualTo(Caber.Models.Enums.RideStatusEnum.Cancelled.ToString()));
            #endregion
        }

        [Test]
        public async Task RateRide()
        {
            #region Arrange
            var passenger = new Passenger()
            {
                UserId = 1
            };

            var cab = new Cab()
            {
                RegistrationNumber = "123",
                Model = "GTR",
                Make = "Nissan",
                DriverId = 1,
                Status = "Idle",
                SeatingCapacity = 3,
                Color = "Red",
                Location = "Lahore"
            };

            Driver driver = new()
            {
                UserId = 1,
                LicenseExpiryDate = DateTime.Now,
                LicenseNumber = "123456"
            };


            var ride = new Ride()
            {
                CabId = 1,
                PassengerId = 1,
                EndLocation = "Test",
                StartLocation = "Test",
                RideStatus = Caber.Models.Enums.RideStatusEnum.Accepted
            };

            GetContext().Passengers.Add(passenger);

            GetContext().Drivers.Add(driver);

            GetContext().Cabs.Add(cab);

            GetContext().Rides.Add(ride);

            GetContext().SaveChanges();

            #endregion

            #region Act
            var result = await RideService.RateRide(new RateRideRequestDto()
            {
                RideId = 1,
                Rating = 5,
                Comment = "Test"
            });
            #endregion

            #region Assert  
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.RideId, Is.EqualTo(1));
                Assert.That(result.PassengerRating, Is.EqualTo(5));
                Assert.That(result.PassengerComment, Is.EqualTo("Test"));
            });
            #endregion
        }
    }
}
