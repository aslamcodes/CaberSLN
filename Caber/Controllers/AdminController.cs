using Caber.Exceptions;
using Caber.Models;
using Caber.Models.DTOs;
using Caber.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Caber.Controllers
{
    [Authorize(Policy = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;
        private readonly ILogger<AdminController> logger;

        public AdminController(IAdminService adminService, ILogger<AdminController> logger)
        {
            this.adminService = adminService;
            this.logger = logger;
        }


        [HttpGet("get-users")]
        [ProducesResponseType(typeof(List<UserProfileResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<UserProfileResponseDto>>> GetAllUsers()
        {
            try
            {
                var users = await adminService.GetAllUsers();

                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("get-cabs")]
        [ProducesResponseType(typeof(List<CabResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CabResponseDto>>> GetCabs()
        {
            try
            {
                var cabs = await adminService.GetCabs();

                return Ok(cabs);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("get-drivers")]
        [ProducesResponseType(typeof(List<DriverDetailsResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<DriverDetailsResponseDto>>> GetDrivers()
        {
            try
            {
                var drivers = await adminService.GetDrivers();

                return Ok(drivers);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("get-passengers")]
        [ProducesResponseType(typeof(List<PassengerResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<PassengerResponseDto>>> GetPassengers()
        {
            try
            {
                var passengers = await adminService.GetPassengers();

                return Ok(passengers);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("verify-driver")]
        [ProducesResponseType(typeof(VerifyDriverResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VerifyDriverResponseDto>> VerifyDriver([FromBody] VerifyDriverRequestDto request)
        {
            try
            {
                var driverVerificationResponse = await adminService.VerifyDriver(request);

                return Ok(driverVerificationResponse);
            }
            catch (DriverNotFoundException e)
            {
                logger.LogError(e, "Driver not found");
                return NotFound(new ErrorModel(e.Message, StatusCodes.Status404NotFound));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
