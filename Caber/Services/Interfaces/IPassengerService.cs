using Caber.Models.DTOs;

namespace Caber.Services.Interfaces
{
    public interface IPassengerService
    {
        Task<PassengerRegisterResponseDto> RegisterPassenger(PassengerRegisterRequestDto passenger);

        Task<RideBasicResponseDto> InitiateRide(InitiatedRideRequestDto request);

        Task<RideCompletedResponseDto> CompleteRide(CompleteRideRequestDto request);

        Task<List<RideWholeResponseDto>> GetRides(int passengerId);
    }
}