using Caber;
using Caber.Contexts;
using Caber.Exceptions;
using Caber.Models;
using Caber.Models.Enums;
using Caber.Repositories;
using Caber.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CaberTests.ServicesTests
{
    public class RoleServiceTest
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
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<CaberContext>()
               .UseInMemoryDatabase("connection")
           .Options;
            SetContext(new CaberContext(options));
            GetContext().Database.EnsureCreated();
        }

        [Test]
        public async Task CanAccessCabTest()
        {
            // Arrange
            var roleService = new RoleService(new CabRepository(GetContext()),
                                                          new DriverRepository(GetContext()),
                                                          new PassengerRepository(GetContext()),
                                           new RideRepository(GetContext()));

            var user = new User()
            {
                Email = "test@test.com",
                FirstName = "test",
                Address = "tester",
                Phone = "1231231231",
                LastName = "test",
                UserType = Caber.Models.Enums.UserTypeEnum.Driver,
                Password = new byte[] { 1, 2, 3, 4 },
                PasswordHashKey = new byte[] { 1, 2, 3, 4 }
            };

            GetContext().Users.Add(user);

            var driver = new Driver()
            {
                LicenseNumber = "123",
                LicenseExpiryDate = new DateTime(2022, 1, 1),
                IsVerified = true,
                DriverStatus = Caber.Models.Enums.DriverStatusEnum.Offline,
                UserId = user.Id,
                LastRide = new DateTime(2021, 1, 1),
                TotalEarnings = 0

            };

            GetContext().Drivers.Add(driver);

            var cab = new Cab()
            {
                RegistrationNumber = "1212",
                Color = "Blue",
                IsVerified = true,
                Location = "123 Avenue",
                SeatingCapacity = 4,
                Status = "Idle",
                Make = "Toyota",
                Model = "Corral",
                DriverId = driver.Id

            };

            GetContext().Cabs.Add(cab);
            GetContext().SaveChanges();

            // Act
            var result = await roleService.CanAccessCab(1, 1);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task CanAccessCabFalseTest()
        {
            // Arrange
            var roleService = new RoleService(new CabRepository(GetContext()),
                                                          new DriverRepository(GetContext()),
                                                          new PassengerRepository(GetContext()),
                                           new RideRepository(GetContext()));

            var user = new User()
            {
                Email = "test@test.com",
                FirstName = "test",
                Address = "tester",
                Phone = "1231231231",
                LastName = "test",
                UserType = Caber.Models.Enums.UserTypeEnum.Driver,
                Password = new byte[] { 1, 2, 3, 4 },
                PasswordHashKey = new byte[] { 1, 2, 3, 4 }
            };

            var user2 = new User()
            {
                Email = "test@test.com",
                FirstName = "test",
                Address = "tester",
                Phone = "1231231231",
                LastName = "test",
                UserType = Caber.Models.Enums.UserTypeEnum.Driver,
                Password = new byte[] { 1, 2, 3, 4 },
                PasswordHashKey = new byte[] { 1, 2, 3, 4 }
            };


            GetContext().Users.Add(user);
            GetContext().Users.Add(user2);

            var driver = new Driver()
            {
                LicenseNumber = "123",
                LicenseExpiryDate = new DateTime(2022, 1, 1),
                IsVerified = true,
                DriverStatus = Caber.Models.Enums.DriverStatusEnum.Offline,
                UserId = 1,
                LastRide = new DateTime(2021, 1, 1),
                TotalEarnings = 0

            };

            var driver2 = new Driver()
            {
                LicenseNumber = "123",
                LicenseExpiryDate = new DateTime(2022, 1, 1),
                IsVerified = true,
                DriverStatus = Caber.Models.Enums.DriverStatusEnum.Offline,
                UserId = 2,
                LastRide = new DateTime(2021, 1, 1),
                TotalEarnings = 0

            };


            GetContext().Drivers.Add(driver);
            GetContext().Drivers.Add(driver2);

            var cab = new Cab()
            {
                RegistrationNumber = "1212",
                Color = "Blue",
                IsVerified = true,
                Location = "123 Avenue",
                SeatingCapacity = 4,
                Status = "Idle",
                Make = "Toyota",
                Model = "Corral",
                DriverId = 1

            };

            GetContext().Cabs.Add(cab);
            GetContext().SaveChanges();

            // Act
            var result = await roleService.CanAccessCab(2, 1);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task CanAccessCabFailTest()
        {
            // Arrange
            var roleService = new RoleService(new CabRepository(GetContext()),
                                                          new DriverRepository(GetContext()),
                                                          new PassengerRepository(GetContext()),
                                           new RideRepository(GetContext()));

            var user = new User()
            {
                Email = "test@test.com",
                FirstName = "test",
                Address = "tester",
                Phone = "1231231231",
                LastName = "test",
                UserType = Caber.Models.Enums.UserTypeEnum.Driver,
                Password = new byte[] { 1, 2, 3, 4 },
                PasswordHashKey = new byte[] { 1, 2, 3, 4 }
            };

            var user2 = new User()
            {
                Email = "test@test.com",
                FirstName = "test",
                Address = "tester",
                Phone = "1231231231",
                LastName = "test",
                UserType = Caber.Models.Enums.UserTypeEnum.Driver,
                Password = new byte[] { 1, 2, 3, 4 },
                PasswordHashKey = new byte[] { 1, 2, 3, 4 }
            };


            GetContext().Users.Add(user);
            GetContext().Users.Add(user2);

            var driver = new Driver()
            {
                LicenseNumber = "123",
                LicenseExpiryDate = new DateTime(2022, 1, 1),
                IsVerified = true,
                DriverStatus = Caber.Models.Enums.DriverStatusEnum.Offline,
                UserId = 1,
                LastRide = new DateTime(2021, 1, 1),
                TotalEarnings = 0

            };

            var driver2 = new Driver()
            {
                LicenseNumber = "123",
                LicenseExpiryDate = new DateTime(2022, 1, 1),
                IsVerified = true,
                DriverStatus = Caber.Models.Enums.DriverStatusEnum.Offline,
                UserId = 2,
                LastRide = new DateTime(2021, 1, 1),
                TotalEarnings = 0

            };


            GetContext().Drivers.Add(driver);
            GetContext().Drivers.Add(driver2);

            var cab = new Cab()
            {
                RegistrationNumber = "1212",
                Color = "Blue",
                IsVerified = true,
                Location = "123 Avenue",
                SeatingCapacity = 4,
                Status = "Idle",
                Make = "Toyota",
                Model = "Corral",
                DriverId = 1

            };

            GetContext().Cabs.Add(cab);
            GetContext().SaveChanges();

            // Act

            // Assert
            Assert.ThrowsAsync<CabNotFoundException>(async () => await roleService.CanAccessCab(2, 3));
        }


        [Test]
        public async Task CanAccessRideForDriverTest()
        {
            // Arrange
            var roleService = new RoleService(new CabRepository(GetContext()),
                                              new DriverRepository(GetContext()),
                                              new PassengerRepository(GetContext()),
                                              new RideRepository(GetContext()));

            var user = new User()
            {
                Email = "driver@test.com",
                FirstName = "Driver",
                Address = "Driver Street",
                Phone = "1231231231",
                LastName = "Test",
                UserType = UserTypeEnum.Driver,
                Password = new byte[] { 1, 2, 3, 4 },
                PasswordHashKey = new byte[] { 1, 2, 3, 4 }
            };

            GetContext().Users.Add(user);
            GetContext().SaveChanges();

            var driver = new Driver()
            {
                LicenseNumber = "123",
                LicenseExpiryDate = new DateTime(2023, 1, 1),
                IsVerified = true,
                DriverStatus = DriverStatusEnum.Offline,
                UserId = user.Id,
                LastRide = new DateTime(2022, 1, 1),
                TotalEarnings = 0
            };

            GetContext().Drivers.Add(driver);
            GetContext().SaveChanges();

            var cab = new Cab()
            {
                RegistrationNumber = "1212",
                Color = "Blue",
                IsVerified = true,
                Location = "123 Avenue",
                SeatingCapacity = 4,
                Status = "Idle",
                Make = "Toyota",
                Model = "Corolla",
                DriverId = driver.Id
            };

            GetContext().Cabs.Add(cab);
            GetContext().SaveChanges();

            var passenger = new Passenger()
            {
                UserId = user.Id
            };

            GetContext().Passengers.Add(passenger);
            GetContext().SaveChanges();

            var ride = new Ride()
            {
                CabId = cab.Id,
                PassengerId = passenger.Id,
                StartLocation = "Start",
                EndLocation = "End",
                Fare = 100,
                RideStatus = RideStatusEnum.Completed,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(30)
            };

            GetContext().Rides.Add(ride);
            GetContext().SaveChanges();

            // Act
            var result = await roleService.CanAccessRide(user.Id, UserTypeEnum.Driver, ride.Id);

            // Assert
            Assert.That(result, Is.True);
        }



        [Test]
        public async Task CanAccessRideForPassengerTest()
        {
            // Arrange
            var roleService = new RoleService(new CabRepository(GetContext()),
                                              new DriverRepository(GetContext()),
                                              new PassengerRepository(GetContext()),
                                              new RideRepository(GetContext()));

            var user = new User()
            {
                Email = "passenger@test.com",
                FirstName = "Passenger",
                Address = "Passenger Street",
                Phone = "1231231231",
                LastName = "Test",
                UserType = UserTypeEnum.Passenger,
                Password = new byte[] { 1, 2, 3, 4 },
                PasswordHashKey = new byte[] { 1, 2, 3, 4 }
            };

            GetContext().Users.Add(user);
            GetContext().SaveChanges();

            var driver = new Driver()
            {
                LicenseNumber = "123",
                LicenseExpiryDate = new DateTime(2023, 1, 1),
                IsVerified = true,
                DriverStatus = DriverStatusEnum.Offline,
                UserId = user.Id,
                LastRide = new DateTime(2022, 1, 1),
                TotalEarnings = 0
            };

            GetContext().Drivers.Add(driver);
            GetContext().SaveChanges();

            var cab = new Cab()
            {
                RegistrationNumber = "1212",
                Color = "Blue",
                IsVerified = true,
                Location = "123 Avenue",
                SeatingCapacity = 4,
                Status = "Idle",
                Make = "Toyota",
                Model = "Corolla",
                DriverId = driver.Id
            };

            GetContext().Cabs.Add(cab);
            GetContext().SaveChanges();

            var passenger = new Passenger()
            {
                UserId = user.Id
            };

            GetContext().Passengers.Add(passenger);
            GetContext().SaveChanges();

            var ride = new Ride()
            {
                CabId = cab.Id,
                PassengerId = passenger.Id,
                StartLocation = "Start",
                EndLocation = "End",
                Fare = 100,
                RideStatus = RideStatusEnum.Completed,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(30)
            };

            GetContext().Rides.Add(ride);
            GetContext().SaveChanges();

            // Act
            var result = await roleService.CanAccessRide(user.Id, UserTypeEnum.Passenger, ride.Id);

            // Assert
            Assert.That(result, Is.True);

        }

        [Test]
        public async Task CanAccessRideForDriverFailTest()
        {

            var roleService = new RoleService(new CabRepository(GetContext()),
                                   new DriverRepository(GetContext()),
                                   new PassengerRepository(GetContext()),
                                   new RideRepository(GetContext()));

            Assert.ThrowsAsync<RideNotFoundException>(async () => await roleService.CanAccessRide(1, UserTypeEnum.Driver, 999));

        }


        [Test]
        public async Task CanAccessRideForPassengerFailTest()
        {
            var roleService = new RoleService(new CabRepository(GetContext()),
                                   new DriverRepository(GetContext()),
                                   new PassengerRepository(GetContext()),
                                   new RideRepository(GetContext()));

            Assert.ThrowsAsync<RideNotFoundException>(async () => await roleService.CanAccessRide(1, UserTypeEnum.Passenger, 999));

        }

        [Test]
        public async Task CanAccessRideForDriverFalseTest()
        {
            // Arrange
            var roleService = new RoleService(new CabRepository(GetContext()),
                                              new DriverRepository(GetContext()),
                                              new PassengerRepository(GetContext()),
                                              new RideRepository(GetContext()));

            var user = new User()
            {
                Email = "driver@test.com",
                FirstName = "Driver",
                Address = "Driver Street",
                Phone = "1231231231",
                LastName = "Test",
                UserType = UserTypeEnum.Driver,
                Password = new byte[] { 1, 2, 3, 4 },
                PasswordHashKey = new byte[] { 1, 2, 3, 4 }
            };

            var anotherUser = new User()
            {
                Email = "anotherdriver@test.com",
                FirstName = "AnotherDriver",
                Address = "Another Driver Street",
                Phone = "3213213211",
                LastName = "Test",
                UserType = UserTypeEnum.Driver,
                Password = new byte[] { 5, 6, 7, 8 },
                PasswordHashKey = new byte[] { 5, 6, 7, 8 }
            };

            GetContext().Users.Add(user);
            GetContext().Users.Add(anotherUser);
            GetContext().SaveChanges();

            var driver = new Driver()
            {
                LicenseNumber = "123",
                LicenseExpiryDate = new DateTime(2023, 1, 1),
                IsVerified = true,
                DriverStatus = DriverStatusEnum.Offline,
                UserId = user.Id,
                LastRide = new DateTime(2022, 1, 1),
                TotalEarnings = 0
            };

            var anotherDriver = new Driver()
            {
                LicenseNumber = "456",
                LicenseExpiryDate = new DateTime(2024, 1, 1),
                IsVerified = true,
                DriverStatus = DriverStatusEnum.Available,
                UserId = anotherUser.Id,
                LastRide = new DateTime(2022, 1, 1),
                TotalEarnings = 0
            };

            GetContext().Drivers.Add(driver);
            GetContext().Drivers.Add(anotherDriver);
            GetContext().SaveChanges();

            var cab = new Cab()
            {
                RegistrationNumber = "1212",
                Color = "Blue",
                IsVerified = true,
                Location = "123 Avenue",
                SeatingCapacity = 4,
                Status = "Idle",
                Make = "Toyota",
                Model = "Corolla",
                DriverId = driver.Id
            };

            GetContext().Cabs.Add(cab);
            GetContext().SaveChanges();

            var passenger = new Passenger()
            {
                UserId = user.Id
            };

            GetContext().Passengers.Add(passenger);
            GetContext().SaveChanges();

            var ride = new Ride()
            {
                CabId = cab.Id,
                PassengerId = passenger.Id,
                StartLocation = "Start",
                EndLocation = "End",
                Fare = 100,
                RideStatus = RideStatusEnum.Completed,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(30)
            };

            GetContext().Rides.Add(ride);
            GetContext().SaveChanges();

            // Act
            var result = await roleService.CanAccessRide(anotherDriver.UserId, UserTypeEnum.Driver, ride.Id);

            // Assert
            Assert.That(result, Is.False);
        }


        [Test]
        public async Task CanAccessRideForPassengerFalseTest()
        {
            // Arrange
            var roleService = new RoleService(new CabRepository(GetContext()),
                                              new DriverRepository(GetContext()),
                                              new PassengerRepository(GetContext()),
                                              new RideRepository(GetContext()));

            var user = new User()
            {
                Email = "passenger1@test.com",
                FirstName = "Passenger1",
                Address = "Passenger1 Street",
                Phone = "1231231231",
                LastName = "Test1",
                UserType = UserTypeEnum.Passenger,
                Password = new byte[] { 1, 2, 3, 4 },
                PasswordHashKey = new byte[] { 1, 2, 3, 4 }
            };

            var anotherUser = new User()
            {
                Email = "passenger2@test.com",
                FirstName = "Passenger2",
                Address = "Passenger2 Street",
                Phone = "3213213211",
                LastName = "Test2",
                UserType = UserTypeEnum.Passenger,
                Password = new byte[] { 5, 6, 7, 8 },
                PasswordHashKey = new byte[] { 5, 6, 7, 8 }
            };

            GetContext().Users.Add(user);
            GetContext().Users.Add(anotherUser);
            GetContext().SaveChanges();

            var driver = new Driver()
            {
                LicenseNumber = "123",
                LicenseExpiryDate = new DateTime(2023, 1, 1),
                IsVerified = true,
                DriverStatus = DriverStatusEnum.Offline,
                UserId = user.Id,
                LastRide = new DateTime(2022, 1, 1),
                TotalEarnings = 0
            };

            GetContext().Drivers.Add(driver);
            GetContext().SaveChanges();

            var cab = new Cab()
            {
                RegistrationNumber = "1212",
                Color = "Blue",
                IsVerified = true,
                Location = "123 Avenue",
                SeatingCapacity = 4,
                Status = "Idle",
                Make = "Toyota",
                Model = "Corolla",
                DriverId = driver.Id
            };

            GetContext().Cabs.Add(cab);
            GetContext().SaveChanges();

            var passenger1 = new Passenger()
            {
                UserId = user.Id
            };

            var passenger2 = new Passenger()
            {
                UserId = anotherUser.Id
            };

            GetContext().Passengers.Add(passenger1);
            GetContext().Passengers.Add(passenger2);
            GetContext().SaveChanges();

            var ride = new Ride()
            {
                CabId = cab.Id,
                PassengerId = passenger1.Id,
                StartLocation = "Start",
                EndLocation = "End",
                Fare = 100,
                RideStatus = RideStatusEnum.Completed,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(30)
            };

            GetContext().Rides.Add(ride);
            GetContext().SaveChanges();

            // Act
            var result = await roleService.CanAccessRide(passenger2.UserId, UserTypeEnum.Passenger, ride.Id);

            // Assert
            Assert.That(result, Is.False);
        }


        [Test]
        public async Task GetPassengerForUserTest()
        {
            // Arrange
            var roleService = new RoleService(new CabRepository(GetContext()),
                                              new DriverRepository(GetContext()),
                                              new PassengerRepository(GetContext()),
                                              new RideRepository(GetContext()));

            var user = new User()
            {
                Email = "passenger@test.com",
                FirstName = "Passenger",
                Address = "Passenger Street",
                Phone = "1231231231",
                LastName = "Test",
                UserType = UserTypeEnum.Passenger,
                Password = new byte[] { 1, 2, 3, 4 },
                PasswordHashKey = new byte[] { 1, 2, 3, 4 }
            };

            GetContext().Users.Add(user);
            GetContext().SaveChanges();

            var passenger = new Passenger()
            {
                UserId = user.Id
            };

            GetContext().Passengers.Add(passenger);
            GetContext().SaveChanges();

            // Act
            var result = await roleService.GetPassengerForUser(user.Id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.UserId, Is.EqualTo(user.Id));
        }

        [Test]
        public async Task GetPassengerForUserFailTest()
        {
            // Arrange
            var roleService = new RoleService(new CabRepository(GetContext()),
                                              new DriverRepository(GetContext()),
                                              new PassengerRepository(GetContext()),
                                              new RideRepository(GetContext()));

            // Act
            var passenger = await roleService.GetPassengerForUser(-1);
            // Assert
            Assert.That(passenger, Is.Null);
        }


        [Test]
        public async Task GetDriverForUserTest()
        {
            // Arrange
            var roleService = new RoleService(new CabRepository(GetContext()),
                                              new DriverRepository(GetContext()),
                                              new PassengerRepository(GetContext()),
                                              new RideRepository(GetContext()));

            var user = new User()
            {
                Email = "driver@test.com",
                FirstName = "Driver",
                Address = "Driver Street",
                Phone = "1231231231",
                LastName = "Test",
                UserType = UserTypeEnum.Driver,
                Password = new byte[] { 1, 2, 3, 4 },
                PasswordHashKey = new byte[] { 1, 2, 3, 4 }
            };

            GetContext().Users.Add(user);
            GetContext().SaveChanges();

            var driver = new Driver()
            {
                LicenseNumber = "123",
                LicenseExpiryDate = new DateTime(2023, 1, 1),
                IsVerified = true,
                DriverStatus = DriverStatusEnum.Offline,
                UserId = user.Id,
                LastRide = new DateTime(2022, 1, 1),
                TotalEarnings = 0
            };

            GetContext().Drivers.Add(driver);
            GetContext().SaveChanges();

            // Act
            var result = await roleService.GetDriverForUser(user.Id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.UserId, Is.EqualTo(user.Id));
        }


        [Test]
        public async Task GetDriverForUserFailTest()
        {
            var roleService = new RoleService(new CabRepository(GetContext()),
                                     new DriverRepository(GetContext()),
                                     new PassengerRepository(GetContext()),
                                     new RideRepository(GetContext()));

            // Act
            var passenger = await roleService.GetDriverForUser(-1);

            // Assert
            Assert.That(passenger, Is.Null);
        }
    }
}
