using Caber;
using Caber.Contexts;
using Caber.Exceptions;
using Caber.Models;
using Caber.Models.DTOs;
using Caber.Models.Enums;
using Caber.Repositories;
using Caber.Services;
using Caber.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CaberTests.ServicesTests
{
    public class PassengerServiceTests
    {
        private CaberContext context;
        private IPassengerService passengerService;
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

            var user2 = new User()
            {
                Email = "driver@gmail.com",
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

            passengerService = new PassengerService(new PassengerRepository(GetContext()),
                                                    new UserRepository(GetContext()),
                                                    new RideRepository(GetContext()));
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

        [Test]
        public async Task RideIntitiateTest()
        {
            #region Arrange

            var passenger = new Passenger()
            {
                UserId = 1
            };

            GetContext().Passengers.Add(passenger);

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

            var ride = new Ride()
            {
                PassengerId = 1,
                CabId = 1,
                PassengerComment = "Great",
                EndLocation = "123",
                StartLocation = "123",
                RideStatus = RideStatusEnum.Accepted
            };

            GetContext().Rides.Add(ride);

            GetContext().SaveChanges();

            await GetContext().SaveChangesAsync();

            #endregion

            #region Act

            var request = new InitiatedRideRequestDto()
            {
                RideId = 1,
            };

            var response = await passengerService.InitiateRide(request);

            #endregion

            #region Assert
            var updatedRide = await GetContext().Rides.FirstOrDefaultAsync(x => x.Id == 1);

            Assert.That(response, Is.Not.Null);
            Assert.That(response.Status, Is.EqualTo(RideStatusEnum.InProgress.ToString()));
            Assert.That(response.RideId, Is.EqualTo(1));

            Assert.That(updatedRide.RideStatus.ToString(), Is.EqualTo(RideStatusEnum.InProgress.ToString()));

            #endregion
        }

        [Test]
        public async Task RideIntitiateFailTest()
        {
            #region Arrange

            var passenger = new Passenger()
            {
                UserId = 1
            };

            GetContext().Passengers.Add(passenger);

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

            var ride = new Ride()
            {
                PassengerId = 1,
                CabId = 1,
                PassengerComment = "Great",
                EndLocation = "123",
                StartLocation = "123",
                RideStatus = RideStatusEnum.Cancelled
            };

            GetContext().Rides.Add(ride);

            GetContext().SaveChanges();

            await GetContext().SaveChangesAsync();

            #endregion

            #region Act

            var request = new InitiatedRideRequestDto()
            {
                RideId = 1,
            };


            #endregion

            #region Assert
            var updatedRide = await GetContext().Rides.FirstOrDefaultAsync(x => x.Id == 1);

            Assert.ThrowsAsync<CannotInitiateRide>(() => passengerService.InitiateRide(request));

            #endregion
        }

        [Test]
        public async Task RideCompleteFailTest()
        {
            #region Arrange

            var passenger = new Passenger()
            {
                UserId = 1
            };

            GetContext().Passengers.Add(passenger);

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

            var ride = new Ride()
            {
                PassengerId = 1,
                CabId = 1,
                PassengerComment = "Great",
                EndLocation = "123",
                StartLocation = "123",
                RideStatus = RideStatusEnum.Accepted
            };

            GetContext().Rides.Add(ride);

            GetContext().SaveChanges();

            await GetContext().SaveChangesAsync();

            #endregion

            #region Act

            var request = new CompleteRideRequestDto()
            {
                RideId = 1,
            };


            #endregion

            #region Assert
            var updatedRide = await GetContext().Rides.FirstOrDefaultAsync(x => x.Id == 1);

            Assert.ThrowsAsync<CannotCompleteRideException>(() => passengerService.CompleteRide(request));

            #endregion

        }

        [Test]
        public async Task RideCompleteTest()
        {
            #region Arrange

            var passenger = new Passenger()
            {
                UserId = 1
            };

            GetContext().Passengers.Add(passenger);

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

            var ride = new Ride()
            {
                PassengerId = 1,
                CabId = 1,
                PassengerComment = "Great",
                EndLocation = "123",
                StartLocation = "123",
                RideStatus = RideStatusEnum.InProgress
            };

            GetContext().Rides.Add(ride);

            GetContext().SaveChanges();

            await GetContext().SaveChangesAsync();

            #endregion

            #region Act

            var request = new CompleteRideRequestDto()
            {
                RideId = 1,
            };

            var response = await passengerService.CompleteRide(request);
            #endregion

            #region Assert
            var updatedRide = await GetContext().Rides.FirstOrDefaultAsync(x => x.Id == 1);

            Assert.That(response, Is.Not.Null);
            Assert.That(response.Status, Is.EqualTo(RideStatusEnum.Completed.ToString()));
            Assert.That(response.RideId, Is.EqualTo(1));
            Assert.That(response.Fare, Is.Not.Null);


            Assert.That(updatedRide.RideStatus.ToString(), Is.EqualTo(RideStatusEnum.Completed.ToString()));
            #endregion

        }

        [Test]
        public async Task GetRidesTest()
        {
            #region Arrange
            var passenger = new Passenger()
            {
                UserId = 1
            };

            GetContext().Passengers.Add(passenger);

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

            var ride = new Ride()
            {
                PassengerId = 1,
                CabId = 1,
                PassengerComment = "Great",
                EndLocation = "123",
                StartLocation = "123",
                RideStatus = RideStatusEnum.Completed
            };

            var ride2 = new Ride()
            {
                PassengerId = 1,
                CabId = 1,
                PassengerComment = "Great",
                EndLocation = "123",
                StartLocation = "123",
                RideStatus = RideStatusEnum.Completed
            };

            GetContext().Rides.Add(ride);
            GetContext().Rides.Add(ride2);

            await GetContext().SaveChangesAsync();

            #endregion

            #region Act
            var results = await passengerService.GetRides(1);
            #endregion

            #region Assert
            Assert.That(results, Is.Not.Null);
            Assert.That(results, Has.Count.EqualTo(2));
            Assert.That(await passengerService.GetRides(3), Has.Count.EqualTo(0));
            #endregion
        }


        [TearDown]
        public void TearDown()
        {
            GetContext().Database.EnsureDeleted();
        }
    }
}
