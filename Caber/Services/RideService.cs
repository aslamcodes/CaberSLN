using Caber.Controllers;
using Caber.Extensions.DtoMappers;
using Caber.Models;
using Caber.Repositories;
using Caber.Services.Interfaces;

namespace Caber.Services
{
    public class RideService(IRepository<int, Ride> rideRepository) : IRideService
    {
        public async Task<RateRideResponseDto> RateRide(RateRideRequestDto rateRide)
        {
            try
            {
                var ride = await rideRepository.GetByKey(rateRide.RideId) ?? throw new RideNotFoundException(rateRide.RideId);

                ride.PassengerRating = rateRide.Rating;
                ride.PassengerComment = rateRide.Comment;

                var ratedRide = await rideRepository.Update(ride);

                return ratedRide.MapToRateRideResponseDto();

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
