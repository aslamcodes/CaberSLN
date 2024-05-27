namespace Caber.Controllers
{
    public class BookCabRequestDto
    {
        public int CabId { get; set; }

        public string PickupLocation { get; set; }

        public string DropoffLocation { get; set; }

        public int PassengerId { get; set; }

    }
}