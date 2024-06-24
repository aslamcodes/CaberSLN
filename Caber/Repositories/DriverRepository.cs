using Caber.Contexts;
using Caber.Exceptions;
using Caber.Models;
using Caber.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Caber.Repositories
{
    public class DriverRepository(CaberContext context) : IRepository<int, Driver>
    {
        public async Task<Driver> Add(Driver entity)
        {
            try
            {
                context.Drivers.Add(entity);
                await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<Driver> Delete(int key)
        {
            try
            {
                var driver = await GetByKey(key);
                context.Drivers.Remove(driver);
                await context.SaveChangesAsync();
                return driver;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Driver>> GetAll()
        {
            try
            {
                var drivers = await context.Drivers.ToListAsync();

                return drivers;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<Driver> GetByKey(int key)
        {
            try
            {
                var driver = await context.Drivers.Include(d => d.OwnedCabs).FirstOrDefaultAsync(d => d.Id == key);

                return driver ?? throw new DriverNotFoundException(key);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<Driver> Update(Driver entity)
        {
            try
            {
                context.Drivers.Update(entity);

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
