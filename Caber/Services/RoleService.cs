using Caber.Models;
using Caber.Models.Enums;
using Caber.Repositories.Interfaces;
using Caber.Services.Interfaces;

namespace Caber.Services
{
    public class RoleService(IRepository<int, Cab> cabRepository,
                             IRepository<int, Driver> driverRepository,
                             IRepository<int, Passenger> passengerRepository,
                             IRepository<int, Ride> rideRepository) : IRoleService
    {
        public async Task<bool> CanAccessCab(int userId, int cabId)
        {
            try
            {
                var cab = await cabRepository.GetByKey(cabId);

                var driver = cab.Driver.UserId;

                if (driver == userId)
                {
                    return true;
                }


                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> CanAccessRide(int userId, UserTypeEnum type, int rideId)
        {
            try
            {
                var ride = await rideRepository.GetByKey(rideId);

                if (type == UserTypeEnum.Driver)
                    return userId == (await driverRepository.GetByKey(ride.Cab.DriverId)).UserId;

                if (type == UserTypeEnum.Passenger)
                    return userId == (await passengerRepository.GetByKey(ride.PassengerId)).UserId;

                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Driver?> GetDriverForUser(int userId)
        {
            try
            {
                return (await driverRepository.GetAll()).FirstOrDefault(p => p.UserId == userId);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<Passenger?> GetPassengerForUser(int userId)
        {
            try
            {
                return (await passengerRepository.GetAll()).FirstOrDefault(p => p.UserId == userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
