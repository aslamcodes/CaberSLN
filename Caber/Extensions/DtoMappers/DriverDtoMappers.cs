using Caber.Controllers;

namespace Caber.Models.DTOs.Mappers
{
    public static class DriverDtoMappers
    {
        public static DriverRegisterResponseDto ToDriverRegisterResponseDto(this Driver driver)
        {
            return new DriverRegisterResponseDto
            {
                DriverId = driver.Id,
                UserId = driver.UserId,
                LicenseNumber = driver.LicenseNumber,
                LicenseExpiryDate = driver.LicenseExpiryDate
            };
        }

        public static DriverDetailsResponseDto ToDriverDetailsResponseDto(this Driver driver)
        {
            return new DriverDetailsResponseDto
            {
                DriverId = driver.Id,
                LicenseNumber = driver.LicenseNumber,
                LicenseExpiryDate = driver.LicenseExpiryDate
            };
        }
    }
}
