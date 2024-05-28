using Caber;
using Caber.Contexts;
using Caber.Controllers;
using Caber.Models;
using Caber.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CaberTests.ServicesTests
{
    public class CabServiceTests
    {
        private CaberContext context;
        private CabService cabService;
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

            var userPassenger = new User()
            {
                Email = "123@gmail.com",
                FirstName = "John",
                Password = new byte[] { 1, 2, 3, 4 },
                PasswordHashKey = new byte[] { 1, 2, 3, 4 },
                Phone = "123123",
                Address = "123"
            };

            var userDriver = new User()
            {
                Email = "123@gmail.com",
                FirstName = "John",
                Password = [1, 2, 3, 4],
                PasswordHashKey = [1, 2, 3, 4],
                Phone = "123123",
                Address = "123"
            };

            var passenger = new Passenger()
            {
                UserId = 1,
            };

            Driver driver = new()
            {
                UserId = 1,
                LicenseExpiryDate = DateTime.Now,
                LicenseNumber = "123456"
            };


            GetContext().Users.Add(userPassenger);
            GetContext().Users.Add(userDriver);

            GetContext().Passengers.Add(passenger);

            GetContext().Drivers.Add(driver);


            GetContext().SaveChanges();

            cabService = new CabService(new CabRepository(GetContext()),
                                        new RideRepository(GetContext()),
                                        new DriverRepository(GetContext()));
        }


        [Test]
        public async Task BookCabTest()
        {
            #region Arrange
            var cab = new Cab()
            {
                RegistrationNumber = "123",
                Model = "GTR",
                Make = "Nissan",
                DriverId = 1,
                Status = "Idle",
                SeatingCapacity = 4,
                Color = "Red",
                Location = "Lahore"
            };

            GetContext().Cabs.Add(cab);
            await GetContext().SaveChangesAsync();

            var request = new BookCabRequestDto()
            {
                CabId = 1,
                PickupLocation = "Koramangala",
                DropoffLocation = "Indiranagar",
                PassengerId = 1
            };
            #endregion

            #region Act
            var response = await cabService.BookCab(request);
            #endregion

            #region Assert
            Assert.That(response, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(response.CabId, Is.EqualTo(request.CabId));
                Assert.That(response.PickupLocation, Is.EqualTo(request.PickupLocation));
                Assert.That(response.DropLocation, Is.EqualTo(request.DropoffLocation));
            });
            #endregion
        }

        [Test]
        public async Task BookCabFailTest()
        {
            #region Arrange
            var request = new BookCabRequestDto()
            {
                CabId = 11,
                PickupLocation = "Koramangala",
                DropoffLocation = "Indiranagar",
                PassengerId = 1
            };
            #endregion

            #region Assert
            Assert.ThrowsAsync<CabNotFoundException>(async () => await cabService.BookCab(request));
            #endregion
        }

        [Test]
        public async Task GetCabsByLocationTests()
        {
            #region Arrange
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
                SeatingCapacity = 4,
                Color = "Red",
                Location = "Lahore"
            };

            GetContext().Cabs.Add(cab1);
            GetContext().Cabs.Add(cab2);
            await GetContext().SaveChangesAsync();
            #endregion

            #region Act
            var response1 = await cabService.GetCabsByLocation("Lahore", 4);
            var response2 = await cabService.GetCabsByLocation("Lahore", 1);
            var response3 = await cabService.GetCabsByLocation("Lahore", 5);
            #endregion

            #region Assert  
            Assert.Multiple(() =>
            {
                Assert.That(response1, Is.Not.Null);
                Assert.That(response2, Is.Not.Null);
                Assert.That(response3, Is.Not.Null);
            });

            Assert.Multiple(() =>
            {
                Assert.That(response1, Has.Count.EqualTo(1));
                Assert.That(response2, Has.Count.EqualTo(2));
                Assert.That(response3, Has.Count.EqualTo(0));
            });
            #endregion
        }

        [Test]
        public async Task GetDriverDetailsTest()
        {
            #region Arrange
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
            GetContext().Cabs.Add(cab1);
            await GetContext().SaveChangesAsync();
            #endregion

            #region Act
            var response = await cabService.GetDriverDetails(1);
            #endregion

            #region Assert
            Assert.That(response, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(response.DriverId, Is.EqualTo(1));
                Assert.That(response.LicenseNumber, Is.EqualTo("123456"));
            });
            #endregion


        }

        [Test]
        public async Task RegisterCabTests()
        {
            #region Arrange
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

            #endregion

            #region Act
            var response = await cabService.RegisterCab(cab1);
            #endregion

            #region Assert
            Assert.That(response, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(response.RegistrationNumber, Is.EqualTo("123"));
                Assert.That(response.DriverId, Is.EqualTo(1));
            });
            #endregion

        }

        [Test]
        public async Task UpdateCabLocationTest()
        {
            #region Arrange
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

            GetContext().Cabs.Add(cab1);
            await GetContext().SaveChangesAsync();
            #endregion

            #region Act
            var response = await cabService.UpdateCabLocation(1, "Koramangala");
            #endregion

            #region Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Location, Is.EqualTo("Koramangala"));
            #endregion
        }

        [Test]
        public async Task UpdateCabFaileTest()
        {
            #region Assert
            Assert.ThrowsAsync<CabNotFoundException>(async () => await cabService.UpdateCabLocation(1, "Koramangala"));
            #endregion
        }

        [Test]
        public async Task UpdateCabProfileTest()
        {
            #region Arrange

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

            GetContext().Cabs.Add(cab1);
            await GetContext().SaveChangesAsync();

            var request = new UpdateCabRequestDto()
            {
                CabId = 1,
                Color = "Blue",
                Make = "Toyota",
                Model = "Corolla",
                SeatingCapacity = 4
            };
            #endregion

            #region Act
            var response = await cabService.UpdateCabProfile(request);
            #endregion

            #region Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.CabId, Is.EqualTo(1));
            Assert.Multiple(() =>
            {
                Assert.That(response.Color, Is.EqualTo("Blue"));
                Assert.That(response.Make, Is.EqualTo("Toyota"));
                Assert.That(response.Model, Is.EqualTo("Corolla"));
                Assert.That(response.SeatingCapacity, Is.EqualTo(4));
            });
            #endregion
        }

        [Test]
        public async Task UpdatePartialCabProfileTest()
        {
            #region Arrange

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

            GetContext().Cabs.Add(cab1);
            await GetContext().SaveChangesAsync();

            var request = new UpdateCabRequestDto()
            {
                CabId = 1,
                Color = "Blue",
                SeatingCapacity = 4
            };
            #endregion

            #region Act
            var response = await cabService.UpdateCabProfile(request);
            #endregion

            #region Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.CabId, Is.EqualTo(1));
            Assert.Multiple(() =>
            {
                Assert.That(response.Color, Is.EqualTo("Blue"));
                Assert.That(response.Make, Is.EqualTo("Nissan"));
                Assert.That(response.Model, Is.EqualTo("GTR"));
                Assert.That(response.SeatingCapacity, Is.EqualTo(4));
            });
            #endregion
        }


        [Test]
        public async Task UpdateCabProfileFailTest()
        {
            #region Arrange
            var request = new UpdateCabRequestDto()
            {
                CabId = 1,
                Color = "Blue",
                SeatingCapacity = 4
            };
            #endregion

            #region Assert
            Assert.ThrowsAsync<CabNotFoundException>(async () => await cabService.UpdateCabProfile(request));
            #endregion
        }

        [TearDown]
        public void TearDown()
        {
            GetContext().Database.EnsureDeleted();
        }
    }
}
