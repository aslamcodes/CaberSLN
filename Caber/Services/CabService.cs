using Caber.Controllers;
using Caber.Extensions;
using Caber.Models;
using Caber.Models.DTOs.Mappers;
using Caber.Repositories;

namespace Caber
{
    public class CabService(IRepository<int, Cab> cabRepository) : ICabService
    {
        public async Task<List<CabResponseDto>> GetCabsByLocation(string location)
        {
            try
            {
                var cabs = await cabRepository.GetAll();

                var cabsByLocation = cabs
                    .Where(c => c.Location.GetSimilarity(location) is >= 0.40)
                    .Select(c => c.MapToCabResponseDto());

                return cabsByLocation.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}