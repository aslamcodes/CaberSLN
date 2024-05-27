using Caber.Controllers;

namespace Caber.Models.DTOs.Mappers
{
    public static class CabDtoMappers
    {
        public static CabResponseDto MapToCabResponseDto(this Cab cab)
        {
            return new CabResponseDto
            {
                CabId = cab.Id,
                DriverId = cab.DriverId,
                Location = cab.Location,
                Status = cab.Status
            };
        }

        public static UpdateCabResponseDto MapToUpdateCabResponseDto(this Cab cab)
        {
            return new UpdateCabResponseDto
            {
                CabId = cab.Id,
                Color = cab.Color,
                SeatingCapacity = cab.SeatingCapacity,
                Model = cab.Model,
                Make = cab.Make
            };
        }
    }
}
