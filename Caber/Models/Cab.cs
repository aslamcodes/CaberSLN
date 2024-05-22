namespace Caber.Models
{
    public class Cab
    {
        public int Id { get; set; }

        public string RegistrationNumber { get; set; }

        public string Model { get; set; }

        public string Make { get; set; }

        public string Color { get; set; }

        public string Status { get; set; }

        public string Location { get; set; }

        public int SeatingCapacity { get; set; }

        public int DriverId { get; set; }

        public Driver Driver { get; set; }

        public ICollection<Ride> Rides { get; set; }
    }
}
