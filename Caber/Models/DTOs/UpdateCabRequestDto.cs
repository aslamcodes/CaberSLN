using System.ComponentModel.DataAnnotations;

namespace Caber.Controllers
{
    public class UpdateCabRequestDto
    {
        [Required(ErrorMessage = "CabId is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Cab id must be greater than 0")]
        public int CabId { get; set; }

        public required string Color { get; set; } = string.Empty;

        [Range(1, 8, ErrorMessage = "Seating Capacity must be between 1 and 8")]
        public int SeatingCapacity { get; set; }

        public string Model { get; set; } = string.Empty;

        public string Make { get; set; } = string.Empty;
    }
}
