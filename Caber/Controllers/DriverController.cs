using Caber.Exceptions;
using Caber.Models;
using Caber.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Caber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController(IDriverService driverService) : Controller
    {
        [HttpGet("ride-ratings-for-driver")]
        public async Task<ActionResult<List<RideRatingResponseDto>>> GetRideRatingsForDriver([FromQuery] int driverId)
        {
            try
            {
                var rideRatings = await driverService.GetDriverRideRatings(driverId);

                return Ok(rideRatings);
            }
            catch (DriverNotFoundException)
            {
                return NotFound(new ErrorModel("Driver not found", StatusCodes.Status404NotFound));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }
        }
    }
}
