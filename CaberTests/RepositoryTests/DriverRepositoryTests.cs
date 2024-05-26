using Caber.Contexts;
using Caber.Exceptions;
using Caber.Models;
using Caber.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CaberTests.RepositoryTests
{
    public class DriverRepositoryTests
    {
        private CaberContext context;

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
        }

        [Test]
        public async Task AddDriver()
        {
            #region Arrange
            IRepository<int, Driver> repository = new DriverRepository(GetContext());

            Driver driver = new()
            {
                UserId = 1,
                OwnedCabs = null,
                LicenseExpiryDate = DateOnly.FromDayNumber(10),
                LicenseNumber = "123456"
            };
            #endregion

            #region Action
            _ = await repository.Add(driver);

            Driver driver1 = await repository.GetByKey(1);
            #endregion

            #region Assert
            Assert.That(driver1, Is.EqualTo(driver));
            Assert.Multiple(() =>
            {
                Assert.That(driver1.UserId, Is.EqualTo(1));
                Assert.That(driver1.LicenseNumber, Is.EqualTo("123456"));
            });
            #endregion
        }

        [Test]
        public async Task DeleteDriver()
        {
            #region Arrange
            IRepository<int, Driver> repository = new DriverRepository(GetContext());

            Driver driver = new()
            {
                UserId = 1,
                OwnedCabs = null,
                LicenseExpiryDate = DateOnly.FromDayNumber(10),
                LicenseNumber = "123456"
            };

            _ = await repository.Add(driver);

            #endregion

            #region Action
            _ = await repository.Delete(1);
            #endregion

            #region Assert
            Assert.ThrowsAsync<DriverNotFoundException>(async () => await repository.GetByKey(1));
            #endregion 
        }

        [Test]
        public async Task GetAllDrivers()
        {
            #region Arrange
            IRepository<int, Driver> repository = new DriverRepository(GetContext());

            Driver driver1 = new Driver()
            {
                UserId = 1,
                OwnedCabs = null,
                LicenseExpiryDate = DateOnly.FromDayNumber(10),
                LicenseNumber = "123456"
            };

            Driver driver2 = new Driver()
            {
                UserId = 2,
                OwnedCabs = null,
                LicenseExpiryDate = DateOnly.FromDayNumber(15),
                LicenseNumber = "789012"
            };

            Driver driver3 = new Driver()
            {
                UserId = 3,
                OwnedCabs = null,
                LicenseExpiryDate = DateOnly.FromDayNumber(20),
                LicenseNumber = "345678"
            };

            _ = await repository.Add(driver1);
            _ = await repository.Add(driver2);
            _ = await repository.Add(driver3);

            #endregion

            #region Action
            var drivers = await repository.GetAll();
            #endregion

            #region Assert
            Assert.That(drivers.Count(), Is.EqualTo(3));
            #endregion
        }

        [Test]
        public async Task UpdateDriverTest()
        {
            #region Arrange
            IRepository<int, Driver> repository = new DriverRepository(GetContext());

            Driver driver = new()
            {
                UserId = 1,
                OwnedCabs = null,
                LicenseExpiryDate = DateOnly.FromDayNumber(10),
                LicenseNumber = "123456"
            };
            _ = await repository.Add(driver);

            #endregion

            #region Action
            var driverDb = await repository.GetByKey(1);
            driverDb.LicenseNumber = "654321";
            driverDb.LicenseExpiryDate = DateOnly.FromDateTime(DateTime.Now.AddDays(10));
            await repository.Update(driverDb);

            #endregion

            #region Assert
            var updatedDriver = await repository.GetByKey(1);
            Assert.Multiple(() =>
            {
                Assert.That(updatedDriver.LicenseNumber, Is.EqualTo("654321"));
                Assert.That(updatedDriver.LicenseExpiryDate, Is.EqualTo(DateOnly.FromDateTime(DateTime.Now.AddDays(10))));
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
