namespace Caber.Controllers
{
    public interface ICabService
    {
        Task<List<CabResponseDto>> GetCabsByLocation(string location);
    }
}