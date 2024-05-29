using Caber;
using Caber.Contexts;
using Caber.Controllers;
using Caber.Models;
using Caber.Models.DTOs;
using Caber.Models.Enums;
using Caber.Services;
using Caber.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CaberTests.ServicesTests
{
    public class RideServiceTests
    {
        private CaberContext context;

        private IRideService rideService;
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

            rideService = new RideService(new RideRepository(GetContext()));
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

            var result = await rideService.CancelRide(cancelRideRequest);
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
            var result = await rideService.RateRide(new RateRideRequestDto()
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


        [Test]
        [TestCase(true, RideStatusEnum.Accepted)]
        [TestCase(false, RideStatusEnum.CancelledByDriver)]
        public async Task AcceptRideTest(bool IsAccepted, RideStatusEnum status)
        {
            #region Arrange
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
                UserId = 2
            };

            GetContext().Passengers.Add(passenger);

            var ride = new Ride()
            {
                PassengerId = 1,
                CabId = 1,
                PassengerComment = "Great",
                EndLocation = "123",
                StartLocation = "123",
                RideStatus = RideStatusEnum.Requested
            };

            GetContext().Rides.Add(ride);
            GetContext().SaveChanges();

            AcceptRideRequestDto request = new()
            {
                RideId = 1,
                Accept = IsAccepted
            };
            #endregion

            #region Act
            var result = await rideService.AcceptRide(request);
            #endregion

            #region Assert
            Assert.That(result, Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(result.RideId, Is.EqualTo(1));
                Assert.That(result.Status, Is.EqualTo(status.ToString()));
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
