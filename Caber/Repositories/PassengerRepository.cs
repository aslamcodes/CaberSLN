using Caber.Contexts;
using Caber.Models;
using Microsoft.EntityFrameworkCore;

namespace Caber.Repositories
{
    public class PassengerRepository(CaberContext context) : IRepository<int, Passenger>
    {
        public async Task<Passenger> Add(Passenger entity)
        {
            try
            {
                context.Passengers.Add(entity);
                await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<Passenger> Delete(int key)
        {
            try
            {
                var passenger = await GetByKey(key);
                context.Passengers.Remove(passenger);
                await context.SaveChangesAsync();
                return passenger;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Passenger>> GetAll()
        {
            try
            {
                var passengers = await context.Passengers.ToListAsync();

                return passengers;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<Passenger> GetByKey(int key)
        {
            try
            {
                var passenger = await context.Passengers.FindAsync(key);

                return passenger ?? throw new PassengerNotFoundException(key);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<Passenger> Update(Passenger entity)
        {
            try
            {
                context.Passengers.Update(entity);

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
