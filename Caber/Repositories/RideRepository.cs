using Caber.Contexts;
using Caber.Exceptions;
using Caber.Models;
using Caber.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Caber
{
    public class RideRepository(CaberContext context) : IRepository<int, Ride>
    {
        public async Task<Ride> Add(Ride entity)
        {
            try
            {
                context.Rides.Add(entity);
                await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Ride> Delete(int key)
        {
            try
            {
                var ride = await GetByKey(key);
                context.Rides.Remove(ride);
                await context.SaveChangesAsync();
                return ride;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Ride>> GetAll()
        {
            try
            {
                var rides = await context.Rides.ToListAsync();

                return rides;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Ride> GetByKey(int key)
        {
            try
            {
                var ride = await context.Rides.FindAsync(key);

                return ride ?? throw new RideNotFoundException(key);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Ride> Update(Ride entity)
        {
            try
            {
                context.Rides.Update(entity);

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