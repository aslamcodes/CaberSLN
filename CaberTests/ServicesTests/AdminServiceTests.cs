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
    public class AdminServiceTests
    {
        private CaberContext context;

        private IAdminService AdminService;
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
                .UseInMemoryDatabase("EmployeeTest")
                .Options;

            SetContext(new CaberContext(options));
            GetContext().Database.EnsureCreated();

            GetContext().SaveChanges();

            AdminService = new AdminService(new DriverRepository(GetContext()),
                                            new PassengerRepository(GetContext()),
                                            new UserRepository(GetContext()),
                                            new CabRepository(GetContext()),
                                            new RideRepository(GetContext()));
        }

        [Test]
        public async Task DriverVerificationTests()
        {
            #region Arrange
            var userDriver = new User()
            {
                Email = "123@gmail.com",
                FirstName = "John",
                Password = new byte[] { 1, 2, 3, 4 },
                PasswordHashKey = new byte[] { 1, 2, 3, 4 },
                Phone = "123123",
                Address = "123"
            };

            Driver driver = new()
            {
                UserId = 1,
                LicenseExpiryDate = DateTime.Now,
                LicenseNumber = "123456"
            };

            await GetContext().Users.AddAsync(userDriver);
            await GetContext().Drivers.AddAsync(driver);
            await GetContext().SaveChangesAsync();
            #endregion

            #region Act
            var request = new VerifyDriverRequestDto()
            {
                DriverId = 1
            };
            var result = await AdminService.VerifyDriver(request);
            #endregion

            #region Assert
            Assert.That(result.IsVerified, Is.True);
            #endregion

        }


        [Test]
        public async Task DriverVerificationTestFail()
        {
            #region Arrange
            var request = new VerifyDriverRequestDto()
            {
                DriverId = 1
            };
            #endregion

            #region Assert
            Assert.ThrowsAsync<DriverNotFoundException>(async () => await AdminService.VerifyDriver(request));
            #endregion

        }

        [Test]
        public async Task GetAllUsersTest()
        {
            #region Arrange
            // Add seed data
            var user1 = new User()
            {
                Email = "user1@gmail.com",
                FirstName = "User1",
                Password = new byte[] { 1, 2, 3, 4 },
                PasswordHashKey = new byte[] { 1, 2, 3, 4 },
                Phone = "1234567890",
                Address = "Address1"
            };
            var user2 = new User()
            {
                Email = "user2@gmail.com",
                FirstName = "User2",
                Password = new byte[] { 5, 6, 7, 8 },
                PasswordHashKey = new byte[] { 5, 6, 7, 8 },
                Phone = "0987654321",
                Address = "Address2"
            };

            await GetContext().Users.AddRangeAsync(user1, user2);
            await GetContext().SaveChangesAsync();
            #endregion

            #region Act
            var result = await AdminService.GetAllUsers();
            #endregion

            #region Assert
            Assert.That(result.Count, Is.EqualTo(2));
            // Add more assertions as needed
            #endregion
        }

        [Test]
        public async Task VerifyCabTest()
        {
            #region Arrange
            // Add seed data
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

            GetContext().Cabs.Add(cab);
            Console.WriteLine(cab.Id);
            GetContext().SaveChanges();

            var request = new VerifyCabRequestDto()
            {
                CabId = 1
            };
            #endregion

            #region Act
            var result = await AdminService.VerifyCab(request);
            #endregion

            #region Assert
            Assert.That(result.CabStatus, Is.EqualTo("Idle"));

            #endregion
        }

        [Test]
        public async Task GetDriversTest()
        {
            #region Arrange

            var driver1 = new Driver()
            {
                UserId = 1,
                LicenseExpiryDate = DateTime.Now.AddDays(30),
                LicenseNumber = "123456"
            };
            var driver2 = new Driver()
            {
                UserId = 2,
                LicenseExpiryDate = DateTime.Now.AddDays(60),
                LicenseNumber = "654321"
            };

            await GetContext().Drivers.AddRangeAsync(driver1, driver2);
            await GetContext().SaveChangesAsync();
            #endregion

            #region Act
            var result = await AdminService.GetDrivers();
            #endregion

            #region Assert
            Assert.That(result, Has.Count.EqualTo(2));
            // Add more assertions as needed
            #endregion
        }

        [Test]
        public async Task GetCabsTest()
        {
            #region Arrange
            // Add seed data
            var cab1 = new Cab()
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
            var cab2 = new Cab()
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

            await GetContext().Cabs.AddAsync(cab1);
            await GetContext().Cabs.AddAsync(cab2);
            await GetContext().SaveChangesAsync();
            #endregion

            #region Act
            var result = await AdminService.GetCabs();
            #endregion

            #region Assert
            Assert.That(result, Has.Count.EqualTo(2));
            // Add more assertions as needed
            #endregion
        }

        [Test]
        public async Task GetRidesInProgressTest()
        {
            #region Arrange
            // Add seed data
            var ride1 = new Ride()
            {
                PassengerId = 1,
                CabId = 1,
                PassengerComment = "Great",
                EndLocation = "123",
                StartLocation = "123",
                RideStatus = Caber.Models.Enums.RideStatusEnum.Requested,

            };
            var ride2 = new Ride()
            {
                PassengerId = 1,
                CabId = 1,
                PassengerComment = "Great",
                EndLocation = "123",
                RideStatus = Caber.Models.Enums.RideStatusEnum.Requested,
                StartLocation = "123"
            };

            GetContext().Rides.Add(ride1);
            GetContext().Rides.Add(ride2);
            GetContext().SaveChanges();
            #endregion

            #region Act
            var result = await AdminService.GetRidesByStatus(Caber.Models.Enums.RideStatusEnum.Requested);
            #endregion

            #region Assert
            Assert.That(result, Has.Count.EqualTo(2));
            #endregion
        }

        [Test]
        public async Task GetPassengersTest()
        {
            #region Arrange
            // Add seed data
            var passenger1 = new Passenger()
            {
                UserId = 1,
            };
            var passenger2 = new Passenger()
            {
                UserId = 2,
            };

            await GetContext().Passengers.AddRangeAsync(passenger1, passenger2);
            await GetContext().SaveChangesAsync();
            #endregion

            #region Act
            var result = await AdminService.GetPassengers();
            #endregion

            #region Assert
            Assert.That(result, Has.Count.EqualTo(2));
            // Add more assertions as needed
            #endregion
        }

        [TearDown]
        public void TearDown()
        {
            GetContext().Database.EnsureDeleted();
        }
    }
}
