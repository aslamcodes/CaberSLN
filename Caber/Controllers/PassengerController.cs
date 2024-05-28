using Caber.Exceptions;
using Caber.Models;
using Caber.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Caber.Controllers
{
    [Authorize(Policy = "Passenger")]
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController(IRideService rideService,
                                     ICabService cabService,
                                     ILogger<PassengerController> logger) : Controller
    {
        [HttpPost("rate-ride")]
        [ProducesResponseType(typeof(RateRideResponseDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<RateRideResponseDto>> RateRide([FromBody] RateRideRequestDto request)
        {
            try
            {
                var ratedRide = await rideService.RateRide(request);
                logger.LogInformation($"Ride with ride id {request.RideId} is rated by passenger");
                return ratedRide;
            }
            catch (RideNotFoundException)
            {
                logger.LogInformation($"Ride with ride id {request.RideId} is not found");
                return NotFound(new ErrorModel("Ride not found", StatusCodes.Status404NotFound));
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }

        [HttpPut("cancel-ride")]
        [ProducesResponseType(typeof(CancelRideResponseDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<CancelRideResponseDto>> CancelRide([FromBody] CancelRideRequestDto request)
        {
            try
            {
                var cancelledRide = await rideService.CancelRide(request);

                return cancelledRide;
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

        [HttpPost("book-cab")]
        [ProducesResponseType(typeof(BookCabResponseDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<BookCabResponseDto>> BookCab([FromBody] BookCabRequestDto request)
        {
            try
            {
                var ride = await cabService.BookCab(request);

                return Ok(ride);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

    }
}
