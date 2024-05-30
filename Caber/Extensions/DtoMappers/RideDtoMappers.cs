using Caber.Controllers;
using Caber.Models;
using Caber.Models.DTOs;

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

        public static CancelRideResponseDto MapToCancelRideResponseDto(this Ride ride)
        {
            return new CancelRideResponseDto()
            {
                RideId = ride.Id,
                Status = ride.RideStatus.ToString()
            };
        }

        public static RideWholeResponseDto MapToRideWholeResponseDto(this Ride ride)
        {
            return new RideWholeResponseDto()
            {
                DriverId = ride.Cab.DriverId,
                PassengerId = ride.PassengerId,
                Id = ride.Id,
                Status = ride.RideStatus.ToString(),
                Fare = ride.Fare.ToString() + "$",
                StartLocation = ride.StartLocation,
                EndLocation = ride.EndLocation,
                StartTime = ride.StartTime.ToString(),
                EndTime = ride.EndTime?.ToString()
            };
        }
    }
}
