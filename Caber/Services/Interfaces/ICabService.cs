namespace Caber.Controllers
{
    public interface ICabService
    {
        Task<List<CabResponseDto>> GetCabsByLocation(string location, int seatingCapacity);

        Task<BookCabResponseDto> BookCab(BookCabRequestDto request);

        Task<DriverDetailsResponseDto> GetDriverDetails(int cabId);
    }
}