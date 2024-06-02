using Caber.Contexts;
using Caber.Models;
using Caber.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Caber
{
    public class CabRepository(CaberContext context) : IRepository<int, Cab>
    {
        public async Task<Cab> Add(Cab entity)
        {
            try
            {
                context.Cabs.Add(entity);
                await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Cab> Delete(int key)
        {
            try
            {
                var cab = await GetByKey(key);
                context.Cabs.Remove(cab);
                await context.SaveChangesAsync();
                return cab;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Cab>> GetAll()
        {
            try
            {
                var cabs = await context.Cabs.Include(c => c.Driver).ToListAsync();

                return cabs;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Cab> GetByKey(int key)
        {
            try
            {
                var cab = await context.Cabs
                                           .Include(c => c.Driver)
                                           .Include(c => c.Rides)
                                           .FirstOrDefaultAsync(c => c.Id == key);

                return cab ?? throw new CabNotFoundException(key);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Cab> Update(Cab entity)
        {
            try
            {
                context.Cabs.Update(entity);

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