namespace Caber.Models.DTOs
{
    public class UpdateCabResponseDto
    {
        public int CabId { get; set; }

        public string Color { get; set; }

        public int SeatingCapacity { get; set; }

        public string Model { get; set; }

        public string Make { get; set; }
    }
}