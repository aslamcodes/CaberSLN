using Caber.Controllers;
using Caber.Models.DTOs;

namespace Caber.Services.Interfaces
{
    public interface IRideService
    {
        Task<RateRideResponseDto> RateRide(RateRideRequestDto rateRide);

        Task<CancelRideResponseDto> CancelRide(CancelRideRequestDto cancelRide);

        Task<AcceptRideResponseDto> AcceptRide(AcceptRideRequestDto request);

    }
}
