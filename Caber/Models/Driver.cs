namespace Caber.Models
{
    public class Driver
    {
        public int Id { get; set; }

        public required string LicenseNumber { get; set; }

        public DateOnly LicenseExpiryDate { get; set; }

        //public ICollection<Cab> OwnedCabs { get; set; }

        //public ICollection<DriverRating> DriverRatings { get; set; }
    }
}
