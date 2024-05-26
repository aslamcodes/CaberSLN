namespace Caber.Models
{
    public class Passenger
    {
        public int Id { get; set; }
        public ICollection<DriverRating> DriverRatings { get; set; }
        public ICollection<Ride> Rides { get; set; }
        public ICollection<FavoritePlaces> FavoritePlaces { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
    }
}