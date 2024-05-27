using Caber.Exceptions;
using Caber.Models;
using Caber.Models.DTOs.Mappers;
using Caber.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Caber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabController(ICabService cabService, IDriverService driverService) : Controller
    {
        [HttpGet()]
        [ProducesResponseType(typeof(CabResponseDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<List<CabResponseDto>>> GetCabsByLocation([FromQuery] string location, [FromQuery] int seatingCapacity)
        {
            try
            {
                var cabs = await cabService.GetCabsByLocation(location, seatingCapacity);

                return Ok(cabs);
            }
            catch (CabNotFoundException)
            {
                return NotFound(new ErrorModel("Cab not found", StatusCodes.Status404NotFound));
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

        [HttpGet("driver-details")]
        [ProducesResponseType(typeof(DriverDetailsResponseDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<DriverDetailsResponseDto>> GetDriverDetails([FromQuery] int cabId)
        {
            try
            {
                var driverDetails = await cabService.GetDriverDetails(cabId);

                return Ok(driverDetails);
            }
            catch (CabNotFoundException)
            {
                return NotFound(new ErrorModel("Cab not found", StatusCodes.Status404NotFound));
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
