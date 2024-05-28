using Caber;
using Caber.Contexts;
using Caber.Models;
using Caber.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CaberTests.RepositoryTests
{
    public class CabRepositoryTests
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
                .UseInMemoryDatabase("CaberCabTests")
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

            Driver driver = new() { LicenseNumber = "123432", UserId = user.Id };
            GetContext().Drivers.Add(driver);

            GetContext().SaveChanges();
        }

        [Test]
        public async Task AddCabTest()
        {
            // Arrange
            Cab cab = new()
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

            // Action
            IRepository<int, Cab> cabrepo = new CabRepository(GetContext());
            var addedCab = await cabrepo.Add(cab);

            // Assert
            Assert.That(addedCab, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(addedCab.RegistrationNumber, Is.EqualTo(cab.RegistrationNumber));
                Assert.That(addedCab.Model, Is.EqualTo(cab.Model));
                Assert.That(addedCab.Make, Is.EqualTo(cab.Make));
                Assert.That(addedCab.DriverId, Is.EqualTo(cab.DriverId));
                Assert.That(addedCab.Status, Is.EqualTo(cab.Status));
                Assert.That(addedCab.SeatingCapacity, Is.EqualTo(cab.SeatingCapacity));
                Assert.That(addedCab.Color, Is.EqualTo(cab.Color));
                Assert.That(addedCab.Location, Is.EqualTo(cab.Location));
            });


        }

        [Test]
        public async Task DeleteCabTest()
        {
            #region Arrange
            IRepository<int, Cab> repository = new CabRepository(GetContext());
            Cab cab = new()
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
            var addedCab = await repository.Add(cab);
            #endregion

            #region Action
            await repository.Delete(addedCab.Id);
            #endregion

            #region Assert
            Assert.ThrowsAsync<CabNotFoundException>(async () => await repository.GetByKey(addedCab.Id));
            #endregion
        }

        [Test]
        public async Task GetAllCabsTest()
        {
            #region Arrange
            IRepository<int, Cab> repository = new CabRepository(GetContext());
            Cab cab = new()
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
            Cab cab2 = new()
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
            await repository.Add(cab);
            await repository.Add(cab2);
            #endregion

            #region Action
            var cabs = await repository.GetAll();
            #endregion

            #region Assert
            Assert.That(cabs, Is.Not.Null);
            Assert.That(cabs.Count(), Is.EqualTo(2));
            #endregion
        }

        [Test]
        public async Task GetCabByKeyTest()
        {
            #region Arrange
            IRepository<int, Cab> repository = new CabRepository(GetContext());
            Cab cab = new()
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
            var addedCab = await repository.Add(cab);
            #endregion

            #region Action
            var cabByKey = await repository.GetByKey(addedCab.Id);
            #endregion

            #region Assert
            Assert.That(cabByKey, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(cabByKey.RegistrationNumber, Is.EqualTo(cab.RegistrationNumber));
                Assert.That(cabByKey.Model, Is.EqualTo(cab.Model));
                Assert.That(cabByKey.Make, Is.EqualTo(cab.Make));
                Assert.That(cabByKey.DriverId, Is.EqualTo(cab.DriverId));
                Assert.That(cabByKey.Status, Is.EqualTo(cab.Status));
                Assert.That(cabByKey.SeatingCapacity, Is.EqualTo(cab.SeatingCapacity));
                Assert.That(cabByKey.Color, Is.EqualTo(cab.Color));
                Assert.That(cabByKey.Location, Is.EqualTo(cab.Location));
            });
            #endregion
        }

        [Test]
        public async Task GetCabByKeyTestFail()
        {
            #region Assert
            Assert.ThrowsAsync<CabNotFoundException>(async () => await new CabRepository(GetContext()).GetByKey(1));
            #endregion
        }

        [Test]
        public async Task UpdateCabTest()
        {
            #region Arrange
            IRepository<int, Cab> repository = new CabRepository(GetContext());
            Cab cab = new()
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
            var addedCab = await repository.Add(cab);
            #endregion

            #region Action
            addedCab.RegistrationNumber = "321";
            var updatedCab = await repository.Update(addedCab);
            #endregion

            #region Assert
            Assert.That(updatedCab, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(updatedCab.RegistrationNumber, Is.EqualTo("321"));
                Assert.That(updatedCab.Model, Is.EqualTo(cab.Model));
                Assert.That(updatedCab.Make, Is.EqualTo(cab.Make));
                Assert.That(updatedCab.DriverId, Is.EqualTo(cab.DriverId));
                Assert.That(updatedCab.Status, Is.EqualTo(cab.Status));
                Assert.That(updatedCab.SeatingCapacity, Is.EqualTo(cab.SeatingCapacity));
                Assert.That(updatedCab.Color, Is.EqualTo(cab.Color));
                Assert.That(updatedCab.Location, Is.EqualTo(cab.Location));
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
