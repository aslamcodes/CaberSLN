namespace Caber.Models.DTOs
{
    public class DriverRegisterResponseDto
    {
        public int DriverId { get; set; }
        public int UserId { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime LicenseExpiryDate { get; set; }
    }
}