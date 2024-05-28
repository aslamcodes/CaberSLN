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

        private IAdminService adminService;
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

            adminService = new AdminService(new DriverRepository(GetContext()));
        }

        [Test]
        public async Task DriverVerificationTests()
        {
            #region Arrange
            var userDriver = new User()
            {
                Email = "123@gmail.com",
                FirstName = "John",
                Password = [1, 2, 3, 4],
                PasswordHashKey = [1, 2, 3, 4],
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
            var result = await adminService.VerifyDriver(request);
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
            Assert.ThrowsAsync<DriverNotFoundException>(async () => await adminService.VerifyDriver(request));
            #endregion

        }



        [TearDown]
        public void TearDown()
        {
            GetContext().Database.EnsureDeleted();
        }
    }
}
