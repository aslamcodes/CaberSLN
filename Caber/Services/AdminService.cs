using Caber.Models;
using Caber.Models.DTOs;
using Caber.Repositories;
using Caber.Services.Interfaces;

namespace Caber.Services
{
    public class AdminService(IRepository<int, Driver> driverRepository) : IAdminService
    {
        public async Task<VerifyDriverResponseDto> VerifyDriver(VerifyDriverRequestDto request)
        {
            try
            {
                var driver = await driverRepository.GetByKey(request.DriverId);

                driver.IsVerified = true;

                await driverRepository.Update(driver);

                return new VerifyDriverResponseDto
                {
                    DriverId = driver.Id,
                    IsVerified = driver.IsVerified
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
