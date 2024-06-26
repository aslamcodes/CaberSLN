namespace Caber.Controllers
{
    public class CabResponseDto
    {
        public int DriverId { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }

        public int Id { get; set; }

        public string RegistrationNumber { get; set; }

        public string Model { get; set; }

        public string Make { get; set; }

        public string Color { get; set; }

        public int SeatingCapacity { get; set; }

        public bool IsVerified { get; set; }
    }
}