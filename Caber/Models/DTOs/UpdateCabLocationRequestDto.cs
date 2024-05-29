using System.ComponentModel.DataAnnotations;

namespace Caber.Controllers
{
    public class UpdateCabLocationRequestDto
    {
        [Required(ErrorMessage = "CabId is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Cab id must be greater than 0")]
        public int CabId { get; set; }

        public required string Location { get; set; }
    }
}
