using System.ComponentModel.DataAnnotations;

namespace Caber.Models.DTOs
{
    public class PassengerRegisterRequestDto
    {
        [Required(ErrorMessage = "Please add UserId to the request.")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be greater than 0.")]
        public int UserId { get; set; }
    }
}