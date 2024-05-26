using Caber.Models;
using Caber.Models.DTOs;
using Caber.Models.DTOs.Mappers;
using Caber.Repositories;
using Caber.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Caber.Services
{
    public class DriverService(IRepository<int, Driver> driverRepository, IRepository<int, User> userRepository) : IDriverService
    {
        public async Task<DriverRegisterResponseDto> RegisterDriver(DriverRegisterRequestDto driver)
        {
            try
            {
                var existingUser = await userRepository.GetByKey(driver.UserId);

                var newDriver = new Driver
                {
                    UserId = existingUser.Id,
                    LicenseNumber = driver.LicenseNumber,
                    LicenseExpiryDate = DateOnly.FromDateTime(driver.LicenseExpiryDate)
                };

                var createdDriver = await driverRepository.Add(newDriver);

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
    }
}