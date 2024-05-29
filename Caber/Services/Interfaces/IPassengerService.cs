using Caber.Models.DTOs;

namespace Caber.Services.Interfaces
{
    public interface IPassengerService
    {
        Task<PassengerRegisterResponseDto> RegisterPassenger(PassengerRegisterRequestDto passenger);

        Task<RideResponseDto> InitiateRide(InitiatedRideRequestDto request);
    }
}