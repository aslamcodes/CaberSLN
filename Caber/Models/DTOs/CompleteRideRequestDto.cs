using System.ComponentModel.DataAnnotations;

namespace Caber.Models.DTOs
{
    public class CompleteRideRequestDto
    {
        [Required(ErrorMessage = "RideId is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "RideId should be greater than 0")]
        public int RideId { get; set; }

        [Required(ErrorMessage = "Please give us rating")]
        [Range(1, 5, ErrorMessage = "Rating should be greater than 0")]
        public int Rating { get; set; }


        public string Comment { get; set; } = string.Empty;

        public float Distance { get; set; }

    }
}