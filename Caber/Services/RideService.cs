using Caber.Controllers;
using Caber.Exceptions;
using Caber.Extensions.DtoMappers;
using Caber.Models;
using Caber.Models.DTOs;
using Caber.Models.Enums;
using Caber.Repositories;
using Caber.Services.Interfaces;

namespace Caber.Services
{
    public class RideService(IRepository<int, Ride> rideRepository) : IRideService
    {
        public async Task<CancelRideResponseDto> CancelRide(CancelRideRequestDto cancelRide)
        {
            try
            {
                var ride = await rideRepository.GetByKey(cancelRide.RideId) ?? throw new RideNotFoundException(cancelRide.RideId);

                ride.RideStatus = RideStatusEnum.Cancelled;

                var cancelledRide = await rideRepository.Update(ride);

                return cancelledRide.MapToCancelRideResponseDto();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<RateRideResponseDto> RateRide(RateRideRequestDto rateRide)
        {
            try
            {
                var ride = await rideRepository.GetByKey(rateRide.RideId) ?? throw new RideNotFoundException(rateRide.RideId);

                ride.PassengerRating = rateRide.Rating;
                ride.PassengerComment = rateRide.Comment ?? null;

                var ratedRide = await rideRepository.Update(ride);

                return ratedRide.MapToRateRideResponseDto();

            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<AcceptRideResponseDto> AcceptRide(AcceptRideRequestDto request)
        {
            try
            {
                var ride = await rideRepository.GetByKey(request.RideId);

                if (ride.RideStatus != RideStatusEnum.Requested)
                    throw new CannotAcceptRideException(ride.RideStatus.ToString());

                if (request.Accept)
                    ride.RideStatus = RideStatusEnum.Accepted;
                else
                    ride.RideStatus = RideStatusEnum.CancelledByDriver;

                var acceptedRide = await rideRepository.Update(ride);

                return new AcceptRideResponseDto()
                {
                    RideId = ride.Id,
                    Status = ride.RideStatus.ToString()
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
