using Caber.Exceptions;
using Caber.Models;
using Caber.Models.DTOs;
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
    public class PassengerController(IRideService rideService,
                                     ICabService cabService,
                                     IPassengerService passengerService,
                                     IRoleService roleService,
                                     ILogger<PassengerController> logger) : Controller
    {
        [Authorize(Policy = "Passenger")]
        [HttpPost("rate-ride")]
        [ProducesResponseType(typeof(RateRideResponseDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<RateRideResponseDto>> RateRide([FromBody] RateRideRequestDto request)
        {
            try
            {
                var userId = User.FindFirst("uid")?.Value;

                if (userId == null)
                {
                    return Unauthorized(new ErrorModel("Unauthorized", StatusCodes.Status401Unauthorized));
                }

                if (!await roleService.CanAccessRide(Convert.ToInt32(userId), UserTypeEnum.Passenger, request.RideId))
                {
                    return Unauthorized(new ErrorModel("Access Denied: Insufficient Permissions", StatusCodes.Status401Unauthorized));
                }


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

        [Authorize(Policy = "Passenger")]
        [HttpPut("cancel-ride")]
        [ProducesResponseType(typeof(CancelRideResponseDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<CancelRideResponseDto>> CancelRide([FromBody] CancelRideRequestDto request)
        {
            try
            {
                var userId = User.FindFirst("uid")?.Value;

                if (userId == null)
                {
                    return Unauthorized(new ErrorModel("Unauthorized", StatusCodes.Status401Unauthorized));
                }

                if (!await roleService.CanAccessRide(Convert.ToInt32(userId), UserTypeEnum.Passenger, request.RideId))
                {
                    return Unauthorized(new ErrorModel("Access Denied: Insufficient Permissions", StatusCodes.Status401Unauthorized));
                }

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

        [Authorize(Policy = "Passenger")]
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

        [Authorize(Policy = "Passenger")]
        [HttpPut("initiate-ride")]
        [ProducesResponseType(typeof(RideBasicResponseDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<RideBasicResponseDto>> InitiateRide([FromBody] InitiatedRideRequestDto request)
        {
            try
            {
                var userId = User.FindFirst("uid")?.Value;

                if (userId == null)
                {
                    return Unauthorized(new ErrorModel("Unauthorized", StatusCodes.Status401Unauthorized));
                }

                if (!await roleService.CanAccessRide(Convert.ToInt32(userId), UserTypeEnum.Passenger, request.RideId))
                {
                    return Unauthorized(new ErrorModel("Access Denied: Insufficient Permissions", StatusCodes.Status401Unauthorized));
                }

                var ride = await passengerService.InitiateRide(request);

                return Ok(ride);
            }
            catch (RideNotFoundException)
            {
                return NotFound(new ErrorModel("Ride not found", StatusCodes.Status404NotFound));
            }
            catch (CannotInitiateRide e)
            {
                return Conflict(new ErrorModel(message: e.Message, code: StatusCodes.Status409Conflict));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


        [Authorize(Policy = "Passenger")]
        [HttpPut("complete-ride")]
        [ProducesResponseType(typeof(RideCompletedResponseDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<RideCompletedResponseDto>> CompleteRide([FromBody] CompleteRideRequestDto request)
        {
            try
            {
                var userId = User.FindFirst("uid")?.Value;

                if (userId == null)
                {
                    return Unauthorized(new ErrorModel("Unauthorized", StatusCodes.Status401Unauthorized));
                }

                if (!await roleService.CanAccessRide(Convert.ToInt32(userId), UserTypeEnum.Passenger, request.RideId))
                {
                    return Unauthorized(new ErrorModel("Access Denied: Insufficient Permissions", StatusCodes.Status401Unauthorized));
                }


                var ride = await passengerService.CompleteRide(request);

                return Ok(ride);
            }
            catch (RideNotFoundException)
            {
                return NotFound(new ErrorModel("Ride not found", StatusCodes.Status404NotFound));
            }
            catch (CannotCompleteRideException e)
            {
                return Conflict(new ErrorModel(message: e.Message, code: StatusCodes.Status409Conflict));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [Authorize(Policy = "Passenger")]
        [HttpGet("ride-history")]
        [ProducesResponseType(typeof(List<RideWholeResponseDto>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<List<RideWholeResponseDto>>> PassengerRide()
        {
            try
            {
                var userId = User.FindFirst("uid")?.Value;

                if (userId == null)
                {
                    return Unauthorized(new ErrorModel("Unauthorized", StatusCodes.Status401Unauthorized));
                }

                var passengerId = roleService.GetPassengerForUser(Convert.ToInt32(userId))?.Id;

                if (passengerId == null)
                {
                    return Unauthorized(new ErrorModel("Unauthorized", StatusCodes.Status401Unauthorized));
                }

                var rides = await passengerService.GetRides((int)passengerId);

                return Ok(rides);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
