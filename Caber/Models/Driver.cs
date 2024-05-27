namespace Caber.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public required string LicenseNumber { get; set; }

        public DateTime LicenseExpiryDate { get; set; }

        public ICollection<Cab> OwnedCabs { get; set; }

        public ICollection<DriverRating> DriverRatings { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public bool IsVerified { get; set; }
    }
}
