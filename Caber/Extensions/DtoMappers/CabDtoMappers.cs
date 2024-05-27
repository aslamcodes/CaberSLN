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
    }
}
