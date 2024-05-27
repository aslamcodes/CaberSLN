namespace Caber.Controllers
{
    public class RateRideResponseDto
    {
        public int RideId { get; set; }
        public int? PassengerRating { get; set; }

        public string? PassengerComment { get; set; }
    }
}