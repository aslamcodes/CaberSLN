using System.ComponentModel.DataAnnotations;

namespace Caber.Models.DTOs
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Id is Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [MinLength(6)]
        public string Password { get; set; }
    }
}