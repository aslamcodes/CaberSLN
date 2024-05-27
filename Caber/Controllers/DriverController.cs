using Caber.Exceptions;
using Caber.Models;
using Caber.Models.DTOs;
using Caber.Models.DTOs.Mappers;
using Caber.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Caber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController(IDriverService driverService, ICabService cabService) : Controller
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


    }
}
