using Caber.Controllers;
using Caber.Models.DTOs;
using Caber.Models.Enums;

namespace Caber.Services.Interfaces
{
    public interface IAdminService
    {
        Task<VerifyDriverResponseDto> VerifyDriver(VerifyDriverRequestDto request);

        Task<List<UserProfileResponseDto>> GetAllUsers();

        Task<VerifyCabResponseDto> VerifyCab(VerifyCabRequestDto request);

        Task<List<DriverDetailsResponseDto>> GetDrivers();

        Task<List<CabResponseDto>> GetCabs();

        Task<List<RideWholeResponseDto>> GetRidesByStatus(RideStatusEnum status);

        Task<List<PassengerResponseDto>> GetPassengers();

    }
}