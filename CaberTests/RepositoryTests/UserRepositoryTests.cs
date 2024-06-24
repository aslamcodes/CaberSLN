using Caber.Contexts;
using Caber.Exceptions;
using Caber.Models;
using Caber.Repositories;
using Caber.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CaberTests.RepositoryTests
{
    public class UserRepositoryTests
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
        }

        [Test]
        public async Task AddUser()
        {
            #region Arrange
            IRepository<int, User> repository = new UserRepository(GetContext());

            User user = new()
            {
                Email = "user@user.com",
                FirstName = "John",
                LastName = "Doe",
                Phone = "123",
                Password = [1, 2, 3, 4],
                PasswordHashKey = [1, 2, 3, 4]
            };
            #endregion

            #region Action
            _ = await repository.Add(user);

            User user1 = await repository.GetByKey(1);
            #endregion

            #region Assert
            Assert.That(user1, Is.EqualTo(user));
            Assert.Multiple(() =>
            {
                Assert.That(user1.Email, Is.EqualTo("user@user.com"));
                Assert.That(user1.FirstName, Is.EqualTo("John"));
                Assert.That(user1.LastName, Is.EqualTo("Doe"));
                Assert.That(user1.Phone, Is.EqualTo("123"));
            });
            #endregion
        }

        [Test]
        public async Task DeleteUser()
        {
            #region Arrange
            IRepository<int, User> repository = new UserRepository(GetContext());

            User user = new User
            {
                Email = "user@user.com",
                FirstName = "John",
                LastName = "Doe",
                Phone = "123",
                Password = [1, 2, 3, 4],
                PasswordHashKey = [1, 2, 3, 4]
            };

            _ = await repository.Add(user);

            #endregion

            #region Action
            _ = await repository.Delete(1);
            #endregion

            #region Assert
            Assert.ThrowsAsync<UserNotFoundException>(async () => await repository.GetByKey(1));
            #endregion 
        }

        [Test]
        public async Task GetAllUsers()
        {
            #region Arrange
            IRepository<int, User> repository = new UserRepository(GetContext());


            User user1 = new()
            {
                Email = "user1@user.com",
                FirstName = "Alice",
                LastName = "Smith",
                Phone = "111-111-1111",
                Password = [1, 2, 3, 4],
                PasswordHashKey = [1, 2, 3, 4]
            };

            User user2 = new()
            {
                Email = "user2@user.com",
                FirstName = "Bob",
                LastName = "Johnson",
                Phone = "222-222-2222",
                Password = [1, 2, 3, 4],
                PasswordHashKey = [1, 2, 3, 4]
            };

            User user3 = new()
            {
                Email = "user3@user.com",
                FirstName = "Charlie",
                LastName = "Brown",
                Phone = "333-333-3333",
                Password = [1, 2, 3, 4],
                PasswordHashKey = [1, 2, 3, 4]
            };

            _ = await repository.Add(user1);
            _ = await repository.Add(user2);
            _ = await repository.Add(user3);

            #endregion

            #region Action
            var users = await repository.GetAll();
            #endregion

            #region Assert
            Assert.That(users.Count(), Is.EqualTo(3));
            #endregion
        }

        [Test]
        public async Task UpdateUserTest()
        {
            #region Arrange
            IRepository<int, User> repository = new UserRepository(GetContext());

            User user = new()
            {
                Email = "user@user.com",
                FirstName = "John",
                LastName = "Doe",
                Phone = "123",
                Password = [1, 2, 3, 4],
                PasswordHashKey = [1, 2, 3, 4]
            };
            _ = await repository.Add(user);

            #endregion

            #region Action
            var userdb = await repository.GetByKey(1);
            userdb.Email = "new@gmail.com";
            userdb.FirstName = "Jane";
            await repository.Update(userdb);
            #endregion

            #region Assert
            var user1 = await repository.GetByKey(1);
            Assert.That(user1.Email, Is.EqualTo("new@gmail.com"));
            Assert.That(user1.FirstName, Is.EqualTo("Jane"));
            #endregion
        }

        [TearDown]
        public void TearDown()
        {
            GetContext().Database.EnsureDeleted();
        }
    }
}
