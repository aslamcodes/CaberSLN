using System.ComponentModel.DataAnnotations;

namespace Caber.Models.DTOs
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        [MinLength(2)]
        public required string FirstName { get; set; }

        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is Required")]
        [MinLength(6)]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Phone is Required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public required string Phone { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}