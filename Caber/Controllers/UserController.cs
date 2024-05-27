using Caber.Exceptions;
using Caber.Models;
using Caber.Models.DTOs;
using Caber.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Caber.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IDriverService driverService, IPassengerService passengerService) : Controller
    {
        [HttpPost("register-driver")]
        [ProducesResponseType(typeof(DriverRegisterResponseDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<DriverRegisterResponseDto>> RegisterDriver([FromBody] DriverRegisterRequestDto driver)
        {
            try
            {
                var registeredDriver = await driverService.RegisterDriver(driver);

                return Ok(registeredDriver);
            }
            catch (UserNotFoundException)
            {
                return BadRequest(new ErrorModel("User not found", StatusCodes.Status400BadRequest));
            }
            catch (DuplicatePassengerException)
            {
                return BadRequest(new ErrorModel("Passenger already registered", StatusCodes.Status400BadRequest));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("register-passenger")]
        [ProducesResponseType(typeof(PassengerRegisterResponseDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<PassengerRegisterResponseDto>> RegisterPassenger([FromBody] PassengerRegisterRequestDto passenger)
        {
            try
            {
                var registeredPassenger = await passengerService.RegisterPassenger(passenger);

                return Ok(registeredPassenger);
            }
            catch (UserNotFoundException)
            {
                return BadRequest(new ErrorModel("User not found", StatusCodes.Status400BadRequest));
            }
            catch (DuplicatePassengerException)
            {
                return BadRequest(new ErrorModel("Passenger already registered", StatusCodes.Status400BadRequest));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
