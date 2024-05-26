using System.ComponentModel.DataAnnotations;

namespace Caber.Models.DTOs
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Id is Required")]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [MinLength(6)]
        public string Password { get; set; }
    }
}