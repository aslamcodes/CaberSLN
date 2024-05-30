using System.ComponentModel.DataAnnotations;

namespace Caber.Models.DTOs
{
    public class VerifyCabRequestDto
    {
        [Required(ErrorMessage = "Cab Id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Cab Id should be greater than 0")]
        public int CabId { get; set; }

    }
}