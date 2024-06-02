using Caber.Exceptions;
using Caber.Models;
using Caber.Models.DTOs;
using Caber.Models.DTOs.Mappers;
using Caber.Models.Enums;
using Caber.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Caber.Controllers
{
    [ExcludeFromCodeCoverage]
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController(IDriverService driverService, ICabService cabService, IRideService rideService, IRoleService roleService) : Controller
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
                var userId = User.FindFirst("uid")?.Value;

                if (userId == null)
                {
                    return Unauthorized(new ErrorModel("Unauthorized", StatusCodes.Status401Unauthorized));
                }

                if (!await roleService.CanAccessCab(Convert.ToInt32(userId), request.CabId))
                {
                    return Unauthorized(new ErrorModel("Access Denied: Insufficient Permissions", StatusCodes.Status401Unauthorized));

                }
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
                var userId = User.FindFirst("uid")?.Value;

                if (userId == null)
                {
                    return Unauthorized(new ErrorModel("Unauthorized", StatusCodes.Status401Unauthorized));
                }

                if (!await roleService.CanAccessRide(Convert.ToInt32(userId), UserTypeEnum.Driver, request.RideId))
                {
                    return Unauthorized(new ErrorModel("Access Denied: Insufficient Permissions", StatusCodes.Status401Unauthorized));
                }


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

        [Authorize(Policy = "Driver")]
        [HttpGet("driver-rides")]
        [ProducesResponseType(typeof(List<RideBasicResponseDto>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<List<RideBasicResponseDto>>> GetDriverRides()
        {
            try
            {
                var userId = User.FindFirst("uid")?.Value;

                var driverId = (await roleService.GetDriverForUser(Convert.ToInt32(userId))).Id;

                var rides = await driverService.GetRidesForDriver(driverId);

                return Ok(rides);
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
        [HttpGet("driver-earnings")]
        [ProducesResponseType(typeof(DriverEarningResponseDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<DriverEarningResponseDto>> GetDriverEarnings()
        {
            try
            {


                var userId = User.FindFirst("uid")?.Value;

                var driverId = (await roleService.GetDriverForUser(Convert.ToInt32(userId))).Id;

                var driverEarnings = await driverService.GetDriverEarnings(driverId);

                return Ok(driverEarnings);
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
