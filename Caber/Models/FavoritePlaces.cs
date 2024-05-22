namespace Caber.Models
{
    public class FavoritePlaces
    {
        public int Id { get; set; }

        public string PlaceName { get; set; }

        public string PlaceAddress { get; set; }

        public int PassengerId { get; set; }

        public Passenger Passenger { get; set; }
    }
}