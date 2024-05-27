namespace Caber.Models.DTOs
{
    public class RideRatingResponseDto
    {
        public int RideId { get; set; }

        public int? PassengerRating { get; set; }

        public string? PassengerComment { get; set; }

    }
}