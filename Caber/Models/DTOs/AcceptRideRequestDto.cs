using System.ComponentModel.DataAnnotations;

namespace Caber.Models.DTOs
{
    public class AcceptRideRequestDto
    {
        [Required(ErrorMessage = "Driver id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Driver id must be greater than 0")]
        public int DriverId { get; set; }

        [Required(ErrorMessage = "Ride id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Ride id must be greater than 0")]
        public int RideId { get; set; }
    }
}
