using System.ComponentModel.DataAnnotations;

namespace Caber.Models.DTOs
{
    public class InitiatedRideRequestDto
    {
        [Required(ErrorMessage = "RideId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "RideId should be above 1")]
        public int RideId { get; set; }
    }
}