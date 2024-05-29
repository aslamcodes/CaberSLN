using Caber.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Caber.Services
{
    public class DeleteUserRequestDto
    {
        [Required(ErrorMessage = "user id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "user type is required")]
        public UserTypeEnum userType { get; set; }
    }
}