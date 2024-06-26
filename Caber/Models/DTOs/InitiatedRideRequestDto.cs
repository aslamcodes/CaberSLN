using System.ComponentModel.DataAnnotations;

namespace Caber.Models.DTOs
{
    public class InitiatedRideRequestDto
    {
        [Required(ErrorMessage = "RideId is required")]
        [Range(0, int.MaxValue, ErrorMessage = "RideId should be above 0")]
        public int RideId { get; set; }
    }
}