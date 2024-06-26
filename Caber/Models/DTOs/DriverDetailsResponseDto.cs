using Caber.Models.DTOs;

namespace Caber.Controllers
{
    public class DriverDetailsResponseDto
    {
        public int DriverId { get; set; }

        public string LicenseNumber { get; set; }

        public DateTime LicenseExpiryDate { get; set; }

        public UserProfileResponseDto user { get; set; }
    }
}