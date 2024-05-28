using Caber.Exceptions;
using Caber.Models;
using Caber.Models.DTOs;
using Caber.Models.DTOs.Mappers;
using Caber.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Caber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController(IDriverService driverService, ICabService cabService, IRideService rideService) : Controller
    {
        [Authorize]
        [HttpGet("ride-ratings-for-driver")]
        [ProducesResponseType(typeof(RideRatingResponseDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
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

        [Authorize(Policy = "Driver")]
        [HttpPut("update-cab")]
        [ProducesResponseType(typeof(CabResponseDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<CabResponseDto>> UpdateCab([FromBody] UpdateCabRequestDto request)
        {
            try
            {
                var updatedCab = await cabService.UpdateCabProfile(request);

                return Ok(updatedCab);
            }
            catch (CabNotFoundException)
            {
                return NotFound(new ErrorModel("Cab not found", StatusCodes.Status404NotFound));
            }
            catch (DriverNotFoundException)
            {
                return NotFound(new ErrorModel("Driver not found", StatusCodes.Status404NotFound));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [Authorize(Policy = "Driver")]
        [HttpPost("register-cab")]
        [ProducesResponseType(typeof(CabResponseDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<CabResponseDto>> RegisterCab([FromBody] RegisterCabRequestDto request)
        {
            try
            {
                var cab = new Cab
                {
                    DriverId = request.DriverId,
                    Color = request.Color,
                    SeatingCapacity = request.SeatingCapacity,
                    Model = request.Model,
                    RegistrationNumber = request.RegistrationNumber,
                    Make = request.Make,
                    Status = "Idle"
                };

                var registerdCab = await cabService.RegisterCab(cab);

                return Ok(registerdCab.MapToCabResponseDto());
            }
            catch (DriverNotFoundException)
            {
                return NotFound(new ErrorModel("Driver not found", StatusCodes.Status404NotFound));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [Authorize(Policy = "Driver")]
        [HttpPut("accept-ride")]
        [ProducesResponseType(typeof(AcceptRideResponseDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<AcceptRideResponseDto>> AcceptRide([FromBody] AcceptRideRequestDto request)
        {
            try
            {
                var acceptedRide = await rideService.AcceptRide(request);

                return Ok(acceptedRide);
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
