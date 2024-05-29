namespace Caber.Models.DTOs
{
    public class RideCompletedResponseDto
    {
        public int RideId { get; set; }

        public int DriverId { get; set; }

        public int PassengerId { get; set; }

        public required string Status { get; set; }

        public required string Fare { get; set; }
    }
}