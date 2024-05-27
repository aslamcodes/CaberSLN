using Caber.Controllers;
using Caber.Models;

namespace Caber.Extensions.DtoMappers
{
    public static class RideMapper
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
    }
}
