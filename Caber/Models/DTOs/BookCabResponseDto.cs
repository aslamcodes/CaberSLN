namespace Caber.Controllers
{
    public class BookCabResponseDto
    {
        public int CabId { get; set; }

        public int DriverId { get; set; }

        public string PickupLocation { get; set; }

        public string DropLocation { get; set; }

        public string Status { get; set; }
    }
}