using Caber.Controllers;
using Caber.Extensions;
using Caber.Extensions.DtoMappers;
using Caber.Models;
using Caber.Models.DTOs.Mappers;
using Caber.Repositories;

namespace Caber
{
    public class CabService(IRepository<int, Cab> cabRepository, IRepository<int, Ride> rideRepository) : ICabService
    {
        public async Task<BookCabResponseDto> BookCab(BookCabRequestDto request)
        {
            try
            {
                var cabDetails = await cabRepository.GetByKey(request.CabId);

                var ride = new Ride()
                {
                    CabId = request.CabId,
                    StartLocation = request.PickupLocation,
                    PassengerId = request.PassengerId,
                    EndLocation = request.DropoffLocation,
                    RideDate = DateTime.Now,
                };

                var bookedRide = await rideRepository.Add(ride);

                return bookedRide.MapToBookCabResponseDto(cabDetails);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<CabResponseDto>> GetCabsByLocation(string location, int seatingCapacity)
        {
            try
            {
                var cabs = await cabRepository.GetAll();

                var cabsByLocation = cabs
                    .Where(c => c.Location.GetSimilarity(location) is >= 0.40)
                    .Where(c => c.SeatingCapacity >= seatingCapacity)
                    .Select(c => c.MapToCabResponseDto());

                return cabsByLocation.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}