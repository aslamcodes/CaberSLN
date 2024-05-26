using Caber.Contexts;
using Caber.Exceptions;
using Caber.Models;
using Caber.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CaberTests.RepositoryTests
{
    public class PassengerRepositoryTests
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
        public async Task AddPassenger()
        {
            #region Arrange
            IRepository<int, Passenger> repository = new PassengerRepository(GetContext());

            Passenger passenger = new()
            {
                UserId = 1,


            };
            #endregion

            #region Action
            _ = await repository.Add(passenger);

            Passenger driver1 = await repository.GetByKey(1);
            #endregion

            #region Assert
            Assert.That(driver1, Is.EqualTo(passenger));
            Assert.Multiple(() =>
            {
                Assert.That(driver1.UserId, Is.EqualTo(1));

            });
            #endregion
        }

        [Test]
        public async Task DeletePassenger()
        {
            #region Arrange
            IRepository<int, Passenger> repository = new PassengerRepository(GetContext());

            Passenger passenger = new()
            {
                UserId = 1,
            };

            _ = await repository.Add(passenger);

            #endregion


            #region Action
            _ = await repository.Delete(1);
            #endregion

            #region Assert
            Assert.ThrowsAsync<PassengerNotFoundException>(async () => await repository.GetByKey(1));
            #endregion 
        }

        [Test]
        public async Task GetAllPassengers()
        {
            #region Arrange
            IRepository<int, Passenger> repository = new PassengerRepository(GetContext());

            Passenger passenger = new()
            {
                UserId = 1,

            };

            _ = await repository.Add(passenger);

            #endregion

            #region Action
            var drivers = await repository.GetAll();
            #endregion

            #region Assert
            Assert.That(drivers.Count(), Is.EqualTo(1));
            #endregion
        }

        [Test]
        public async Task UpdatePassengerTest()
        {
            #region Arrange
            IRepository<int, Passenger> repository = new PassengerRepository(GetContext());

            Passenger passenger = new()
            {
                UserId = 1,

            };
            _ = await repository.Add(passenger);

            #endregion

            #region Action
            var passengerDb = await repository.GetByKey(1);

            await repository.Update(passengerDb);

            #endregion

            #region Assert
            var updatedPassenger = await repository.GetByKey(1);
            Assert.Multiple(() =>
            {
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
