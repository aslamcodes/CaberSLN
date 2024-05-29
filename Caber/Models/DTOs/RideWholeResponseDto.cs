namespace Caber.Models.DTOs
{
    public class RideWholeResponseDto
    {
        public int Id { get; set; }

        public int PassengerId { get; set; }

        public int DriverId { get; set; }

        public string? StartLocation { get; set; }

        public string? EndLocation { get; set; }

        public string? StartTime { get; set; }

        public string? EndTime { get; set; }

        public string? Status { get; set; }

        public string? Fare { get; set; }
    }
}