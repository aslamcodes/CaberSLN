using System.ComponentModel.DataAnnotations;

namespace Caber.Models.DTOs
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        [MinLength(2)]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [MinLength(6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phone is Required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}