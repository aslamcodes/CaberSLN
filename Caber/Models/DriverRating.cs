namespace Caber.Models
{
    public class DriverRating
    {
        public int Id { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public int PassengerId { get; set; }

        public Passenger Passenger { get; set; }

        public int DriverId { get; set; }

        public Driver Driver { get; set; }
    }
}