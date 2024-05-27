using Caber.Controllers;
using Caber.Models;

namespace Caber.Extensions.DtoMappers
{
    public static class RideDtoMappers
    {
        public static BookCabResponseDto MapToBookCabResponseDto(this Ride ride, Cab cab)
        {
            return new BookCabResponseDto()
            {
                CabId = ride.CabId,
                DriverId = cab.DriverId,
                PickupLocation = ride.StartLocation,
                DropLocation = ride.EndLocation,
                Status = ride.RideStatus.ToString()
            };
        }

        public static RateRideResponseDto MapToRateRideResponseDto(this Ride ride)
        {
            return new RateRideResponseDto()
            {
                RideId = ride.Id,
                PassengerRating = ride.PassengerRating,
                PassengerComment = ride.PassengerComment
            };
        }

        public static RideRatingResponseDto MapToRideRatingResponseDto(this Ride ride)
        {
            return new RideRatingResponseDto()
            {
                RideId = ride.Id,
                PassengerRating = ride.PassengerRating,
                PassengerComment = ride.PassengerComment
            };
        }
    }
}
