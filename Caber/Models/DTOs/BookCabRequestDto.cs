using System.ComponentModel.DataAnnotations;

namespace Caber.Controllers
{
    public class BookCabRequestDto
    {
        [Required(ErrorMessage = "CabId is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Cab id must be greater than 0")]
        public int CabId { get; set; }

        [Required(ErrorMessage = "Pickup location is required")]
        public required string PickupLocation { get; set; }

        [Required(ErrorMessage = "Dropoff location is required")]
        public required string DropoffLocation { get; set; }

        [Required(ErrorMessage = "Passenger id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Passenger id must be greater than 0")]
        public int PassengerId { get; set; }

    }
}