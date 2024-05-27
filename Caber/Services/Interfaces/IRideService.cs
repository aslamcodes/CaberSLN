using Caber.Controllers;

namespace Caber.Services.Interfaces
{
    public interface IRideService
    {
        Task<RateRideResponseDto> RateRide(RateRideRequestDto rateRide);
    }
}
