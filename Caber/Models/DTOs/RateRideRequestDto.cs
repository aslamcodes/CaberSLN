using System.ComponentModel.DataAnnotations;

namespace Caber.Controllers
{
    public class RateRideRequestDto
    {
        [Required(ErrorMessage = "RideId is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Ride id must be greater than 0")]
        public int RideId { get; set; }

        [Required(ErrorMessage = "Rating is Required")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }

        public string? Comment { get; set; }
    }
}