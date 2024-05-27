namespace Caber.Controllers
{
    public class RegisterCabRequestDto
    {
        public string RegistrationNumber { get; set; }

        public string Model { get; set; }

        public string Make { get; set; }

        public string Color { get; set; }

        public int SeatingCapacity { get; set; }

        public int DriverId { get; set; }

    }
}