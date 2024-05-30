using Caber.Models.DTOs;

namespace Caber.Services.Interfaces
{
    public interface IDriverService
    {
        Task<DriverRegisterResponseDto> RegisterDriver(DriverRegisterRequestDto driver);

        Task<List<RideRatingResponseDto>> GetDriverRideRatings(int driverId);

        Task<List<RideBasicResponseDto>> GetRidesForDriver(int driverId);

        Task<DriverEarningResponseDto> GetDriverEarnings(int driverId);

        Task<DriverStatusUpdateResponseDto> UpdateDriverStatus(DriverStatusUpdateRequestDto request);
    }
}