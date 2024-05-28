using Caber;
using Caber.Contexts;
using Caber.Models;
using Caber.Repositories;
using Caber.Services;
using Microsoft.EntityFrameworkCore;

namespace CaberTests.RepositoryTests
{
    public class RideRepositoryTests
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
                .UseInMemoryDatabase("CaberRideTests")
                .Options;

            SetContext(new CaberContext(options));
            GetContext().Database.EnsureCreated();

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

            var user = new User()
            {
                Email = "123@gmail.com",
                FirstName = "John",
                Password = new byte[] { 1, 2, 3, 4 },
                PasswordHashKey = new byte[] { 1, 2, 3, 4 },
                Phone = "123123",
                Address = "123"
            };

            var passenger = new Passenger()
            {
                UserId = 1,
            };


            GetContext().Cabs.Add(cab);
            GetContext().Users.Add(user);
            GetContext().Passengers.Add(passenger);

        }

        [Test]
        public async Task AddRideTest()
        {
            #region Arrange
            IRepository<int, Ride> repository = new RideRepository(GetContext());

            var cab = repository.GetByKey(1);
            var passenger = repository.GetByKey(1);

            #endregion

            #region Action
            Ride rideToAdd = new()
            {
                CabId = 1,
                PassengerId = 1,
                StartLocation = "Lahore",
                EndLocation = "Islamabad",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                RideStatus = Caber.Models.Enums.RideStatusEnum.Requested,
                Fare = 1000
            };
            var ride = await repository.Add(rideToAdd);
            #endregion

            #region Assert
            Assert.That(ride, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(ride.CabId, Is.EqualTo(1));
                Assert.That(ride.PassengerId, Is.EqualTo(1));
                Assert.That(ride.StartLocation, Is.EqualTo("Lahore"));
                Assert.That(ride.EndLocation, Is.EqualTo("Islamabad"));
                Assert.That(ride.StartTime, Is.EqualTo(rideToAdd.StartTime));
                Assert.That(ride.EndTime, Is.EqualTo(rideToAdd.EndTime));
                Assert.That(ride.RideStatus, Is.EqualTo(Caber.Models.Enums.RideStatusEnum.Requested));
                Assert.That(ride.Fare, Is.EqualTo(1000));
            });
            #endregion

        }

        [Test]
        public async Task DeleteRideTest()
        {
            #region Arrange
            IRepository<int, Ride> repository = new RideRepository(GetContext());

            Ride rideToAdd = new()
            {
                CabId = 1,
                PassengerId = 1,
                StartLocation = "Lahore",
                EndLocation = "Islamabad",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                RideStatus = Caber.Models.Enums.RideStatusEnum.Requested,
                Fare = 1000
            };
            var ride = await repository.Add(rideToAdd);
            #endregion

            #region Action
            var deletedRide = await repository.Delete(1);
            #endregion

            #region Assert
            Assert.That(deletedRide, Is.Not.Null);
            Assert.ThrowsAsync<RideNotFoundException>(async () => await repository.GetByKey(1));
            #endregion
        }

        [Test]
        public async Task DeleteRideFailTest()
        {
            #region Arrange
            IRepository<int, Ride> repository = new RideRepository(GetContext());
            #endregion

            #region Assert
            Assert.ThrowsAsync<RideNotFoundException>(async () => await repository.GetByKey(1));
            #endregion
        }

        [Test]
        public async Task UpdateRideTest()
        {
            #region Arrange
            IRepository<int, Ride> rideRepo = new RideRepository(GetContext());

            Ride rideToAdd = new()
            {
                CabId = 1,
                PassengerId = 1,
                StartLocation = "Lahore",
                EndLocation = "Islamabad",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                RideStatus = Caber.Models.Enums.RideStatusEnum.Requested,
                Fare = 1000
            };

            await rideRepo.Add(rideToAdd);
            #endregion

            #region Action
            Ride rideToUpdate = await rideRepo.GetByKey(1);
            rideToUpdate.RideStatus = Caber.Models.Enums.RideStatusEnum.Completed;
            var updatedRide = await rideRepo.Update(rideToUpdate);
            #endregion

            #region Assert
            Assert.That(updatedRide, Is.Not.Null);
            Assert.That(updatedRide.RideStatus, Is.EqualTo(Caber.Models.Enums.RideStatusEnum.Completed));
            #endregion
        }

        [Test]
        public async Task GetByIdTest()
        {
            #region Arrange
            IRepository<int, Ride> rideRepo = new RideRepository(GetContext());

            Ride rideToAdd = new()
            {
                CabId = 1,
                PassengerId = 1,
                StartLocation = "Lahore",
                EndLocation = "Islamabad",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                RideStatus = Caber.Models.Enums.RideStatusEnum.Requested,
                Fare = 1000
            };
            await rideRepo.Add(rideToAdd);

            #endregion

            #region Action
            Ride ride = await rideRepo.GetByKey(1);

            #endregion

            #region Assert
            Assert.That(ride, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(ride.CabId, Is.EqualTo(1));
                Assert.That(ride.PassengerId, Is.EqualTo(1));
                Assert.That(ride.StartLocation, Is.EqualTo("Lahore"));
                Assert.That(ride.EndLocation, Is.EqualTo("Islamabad"));
                Assert.That(ride.StartTime, Is.EqualTo(rideToAdd.StartTime));
                Assert.That(ride.EndTime, Is.EqualTo(rideToAdd.EndTime));
                Assert.That(ride.RideStatus, Is.EqualTo(Caber.Models.Enums.RideStatusEnum.Requested));
                Assert.That(ride.Fare, Is.EqualTo(1000));
            });
            #endregion
        }

        [Test]
        public async Task TestGetAll()
        {
            #region Arrange
            IRepository<int, Ride> rideRepo = new RideRepository(GetContext());

            Ride rideToAdd = new()
            {
                CabId = 1,
                PassengerId = 1,
                StartLocation = "Lahore",
                EndLocation = "Islamabad",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                RideStatus = Caber.Models.Enums.RideStatusEnum.Requested,
                Fare = 1000
            };
            Ride rideToAdd2 = new()
            {
                CabId = 1,
                PassengerId = 1,
                StartLocation = "Lahore",
                EndLocation = "Islamabad",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                RideStatus = Caber.Models.Enums.RideStatusEnum.Requested,
                Fare = 1000
            };
            _ = rideRepo.Add(rideToAdd);
            _ = rideRepo.Add(rideToAdd2);
            #endregion

            #region Action
            var rides = await rideRepo.GetAll();
            #endregion

            #region Assert
            Assert.That(rides, Is.Not.Null);
            Assert.That(rides.Count(), Is.EqualTo(2));
            #endregion
        }


        [TearDown]
        public void TearDown()
        {
            GetContext().Database.EnsureDeleted();
        }
    }
}
