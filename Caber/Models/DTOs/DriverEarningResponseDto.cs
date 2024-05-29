namespace Caber.Models.DTOs
{
    public class DriverEarningResponseDto
    {
        public int DriverId { get; set; }

        public required string Earnings { get; set; }
    }
}