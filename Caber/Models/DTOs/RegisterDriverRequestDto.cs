using System.ComponentModel.DataAnnotations;

namespace Caber.Models.DTOs
{
    public class RegisterDriverRequestDto : RegisterRequestDto
    {
        [Required(ErrorMessage = "License is required")]
        public string LicenseNumber { get; set; }

        [Required(ErrorMessage = "License Expiry Date is required")]
        public DateTime LicenseExpiryDate { get; set; }
    }
}