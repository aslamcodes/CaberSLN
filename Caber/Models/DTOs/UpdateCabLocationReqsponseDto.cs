namespace Caber.Models.DTOs
{
    public class UpdateCabLocationReqsponseDto
    {
        public int CabId { get; set; }

        public required string Status { get; set; }

        public required string Location { get; set; }
    }
}