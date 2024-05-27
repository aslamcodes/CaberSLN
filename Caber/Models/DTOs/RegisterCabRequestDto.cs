using System.ComponentModel.DataAnnotations;

namespace Caber.Controllers
{
    public class RegisterCabRequestDto
    {
        [Required(ErrorMessage = "Registration Number is Required")]
        public string RegistrationNumber { get; set; }

        [Required(ErrorMessage = "Model is Required")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Make is Required")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Color is Required")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Seating Capacity is Required")]
        [Range(1, 8, ErrorMessage = "Seating Capacity must be between 1 and 8")]

        public int SeatingCapacity { get; set; }

        [Required(ErrorMessage = "Driver Id is Required")]
        public int DriverId { get; set; }

    }
}