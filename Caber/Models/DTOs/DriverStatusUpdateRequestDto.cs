using Caber.Models.Enums;

namespace Caber.Models.DTOs
{
    public class DriverStatusUpdateRequestDto
    {
        public int DriverId { get; set; }
        public DriverStatusEnum Status { get; set; }
    }
}