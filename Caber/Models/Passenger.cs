namespace Caber.Models
{
    public class Passenger : User
    {
        public ICollection<DriverRating> DriverRatings { get; set; }
        public ICollection<Ride> Rides { get; set; }
        public ICollection<FavoritePlaces> FavoritePlaces { get; set; }
    }
}