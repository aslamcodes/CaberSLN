using Caber.Models;
using Caber.Models.DTOs;
using Caber.Models.DTOs.Mappers;
using Caber.Models.Enums;
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

                existingUser.UserType = UserTypeEnum.Driver;

                await userRepository.Update(existingUser);

                var newDriver = new Driver
                {
                    UserId = existingUser.Id,
                    LicenseNumber = driver.LicenseNumber,
                    LicenseExpiryDate = driver.LicenseExpiryDate
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