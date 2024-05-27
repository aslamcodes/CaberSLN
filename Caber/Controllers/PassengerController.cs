using Caber.Models;
using Caber.Services;
using Caber.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Caber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController(IRideService rideService) : Controller
    {
        [HttpPost("rate-ride")]
        [ProducesResponseType(typeof(RateRideResponseDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<RateRideResponseDto>> RateRide([FromBody] RateRideRequestDto request)
        {
            try
            {
                var ratedRide = await rideService.RateRide(request);

                return ratedRide;
            }
            catch (RideNotFoundException)
            {
                return NotFound(new ErrorModel("Ride not found", StatusCodes.Status404NotFound));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
