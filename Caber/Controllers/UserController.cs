using Caber.Exceptions;
using Caber.Models;
using Caber.Models.DTOs;
using Caber.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Caber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IDriverService driverService) : Controller
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
            catch (DuplicateDriverException)
            {
                return BadRequest(new ErrorModel("Driver already registered", StatusCodes.Status400BadRequest));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

    }
}
