using Caber.Models;
using Caber.Models.DTOs;

namespace Caber.Controllers
{
    public interface ICabService
    {
        Task<List<CabResponseDto>> GetCabsByLocation(string location, int seatingCapacity);

        Task<BookCabResponseDto> BookCab(BookCabRequestDto request);

        Task<Ride> BookAnyCab(int passengerId, string pickupLocation, string dropoffLocation, int seatingCapacity);

        Task<DriverDetailsResponseDto> GetDriverDetails(int cabId);

        Task<Cab> RegisterCab(Cab cab);

        Task<Cab> UpdateCabLocation(int cabId, string location);

        Task<UpdateCabResponseDto> UpdateCabProfile(UpdateCabRequestDto cab);

        Task<List<Cab>> GetCabsForDriver(int driverId);

    }
}