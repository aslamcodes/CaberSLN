namespace Caber.Models.DTOs
{
    public class RideResponseDto
    {
        public int RideId { get; set; }

        public int DriverId { get; set; }

        public int PassengerId { get; set; }

        public string Status { get; set; }
    }
}