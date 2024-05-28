using Caber.Extensions.DtoMappers;
using Caber.Models;
using Caber.Models.DTOs;
using Caber.Models.DTOs.Mappers;
using Caber.Models.Enums;
using Caber.Repositories;
using Caber.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Caber.Services
{
    public class DriverService(IRepository<int, Driver> driverRepository,
                               IRepository<int, User> userRepository,
                               IRepository<int, Cab> cabRepository) : IDriverService
    {
        public async Task<DriverRegisterResponseDto> RegisterDriver(DriverRegisterRequestDto driver)
        {

            try
            {
                var existingUser = await userRepository.GetByKey(driver.UserId);

                existingUser.UserType = UserTypeEnum.Driver;

                var newDriver = new Driver
                {
                    UserId = existingUser.Id,
                    LicenseNumber = driver.LicenseNumber,
                    LicenseExpiryDate = driver.LicenseExpiryDate
                };

                var createdDriver = await driverRepository.Add(newDriver);

                await userRepository.Update(existingUser);

                return createdDriver.ToDriverRegisterResponseDto();

            }
            catch (DbUpdateException)
            {
                throw new DuplicateDriverException();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<List<RideRatingResponseDto>> GetDriverRideRatings(int driverId)
        {
            try
            {
                var driver = await driverRepository.GetByKey(driverId);

                List<RideRatingResponseDto> rideRatings = [];

                foreach (var cab in driver.OwnedCabs)
                {
                    foreach (var ride in (await cabRepository.GetByKey(cab.Id)).Rides)
                    {
                        rideRatings.Add(ride.MapToRideRatingResponseDto());
                    }

                }

                return rideRatings;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}