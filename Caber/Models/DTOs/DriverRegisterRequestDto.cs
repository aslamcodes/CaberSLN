using System.ComponentModel.DataAnnotations;

namespace Caber.Models.DTOs
{
    public class DriverRegisterRequestDto
    {
        [Required(ErrorMessage = "Please add UserId to the request.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please add LicenseNumber to the request.")]
        public string LicenseNumber { get; set; }

        [Required(ErrorMessage = "Please add LicenseExpiryDate to the request.")]
        [DataType(DataType.Date)]
        public DateTime LicenseExpiryDate { get; set; }
    }
}