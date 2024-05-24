using Caber.Contexts;
using Caber.Exceptions;
using Caber.Models;
using Microsoft.EntityFrameworkCore;

namespace Caber.Repositories
{
    public class UserRepository(CaberContext context) : IRepository<int, User>
    {
        public async Task<User> Add(User entity)
        {
            try
            {
                context.Users.Add(entity);
                await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<User> Delete(int key)
        {
            try
            {
                var user = await GetByKey(key);
                context.Users.Remove(user);
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                var users = await context.Users.ToListAsync();

                return users;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<User> GetByKey(int key)
        {
            try
            {
                var user = await context.Users.FindAsync(key);

                return user ?? throw new UserNotFoundException(key);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<User> Update(User entity)
        {
            try
            {
                context.Users.Update(entity);

                await context.SaveChangesAsync();

                return entity;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
