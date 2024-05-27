using System.ComponentModel.DataAnnotations;

namespace Caber.Models.DTOs
{
    public class VerifyDriverRequestDto
    {
        [Required(ErrorMessage = "Driver Id is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Driver id must be greater than 0")]
        public int DriverId { get; set; }
    }
}