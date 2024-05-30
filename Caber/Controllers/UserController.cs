using Caber.Exceptions;
using Caber.Models;
using Caber.Models.DTOs;
using Caber.Services;
using Caber.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Caber.Controllers
{
    [ExcludeFromCodeCoverage]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IDriverService driverService,
                                IPassengerService passengerService,
                                IUserService userService,
                                ILogger<UserController> logger) : Controller
    {
        [HttpPost("register-driver")]
        [ProducesResponseType(typeof(DriverRegisterResponseDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<DriverRegisterResponseDto>> RegisterDriver([FromBody] DriverRegisterRequestDto driver)
        {
            try
            {
                var registeredDriver = await driverService.RegisterDriver(driver);
                logger.LogInformation("Driver Registered");
                return Ok(registeredDriver);
            }
            catch (UserNotFoundException e)
            {
                logger.LogError($"User for id {driver.UserId} not found", e.StackTrace);
                return BadRequest(new ErrorModel("User not found", StatusCodes.Status400BadRequest));
            }
            catch (DuplicateDriverException e)
            {
                logger.LogError($"Driver is already registered on {driver.UserId}", e.StackTrace);
                return BadRequest(new ErrorModel("Passenger already registered", StatusCodes.Status400BadRequest));
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e.StackTrace);
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
                logger.LogInformation($"Passenger register for user with id ${passenger.UserId}");
                return Ok(registeredPassenger);
            }
            catch (UserNotFoundException)
            {

                logger.LogError($"User for id {passenger.UserId} not found");
                return BadRequest(new ErrorModel("User not found", StatusCodes.Status400BadRequest));
            }
            catch (DuplicatePassengerException)
            {
                logger.LogError($"Passenger is already registered on {passenger.UserId}");
                return BadRequest(new ErrorModel("Passenger already registered", StatusCodes.Status400BadRequest));
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }

        [HttpPut("update-profile")]
        [ProducesResponseType(typeof(UserProfileUpdateResponseDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<UserProfileUpdateResponseDto>> UpdateProfile([FromBody] UserProfileUpdateRequestDto request)
        {
            try
            {
                var updatedProfile = await userService.UpdateUserProfile(request);
                logger.LogInformation($"Profile updated for user with id ${request.Id}");
                return Ok(updatedProfile);
            }
            catch (UserNotFoundException)
            {
                logger.LogError($"User for id {request.Id} not found");
                return BadRequest(new ErrorModel("User not found", StatusCodes.Status400BadRequest));
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }

        [HttpGet("profile")]
        [ProducesResponseType(typeof(UserProfileResponseDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<UserProfileResponseDto>> GetProfile([FromQuery] int userId)
        {
            try
            {
                var profile = await userService.GetUserProfile(userId);
                logger.LogInformation($"Profile retrieved for user with id ${userId}");
                return Ok(profile);
            }
            catch (UserNotFoundException)
            {
                logger.LogError($"User for id {userId} not found");
                return BadRequest(new ErrorModel("User not found", StatusCodes.Status400BadRequest));
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }

        //[HttpDelete]
        //[ProducesResponseType(typeof(DeleteUserResponseDto), StatusCodes.Status200OK)]
        //[ProducesErrorResponseType(typeof(ErrorModel))]
        //public async Task<ActionResult<DeleteUserResponseDto>> DeleteUser([FromBody] DeleteUserRequestDto userDetails)
        //{
        //    try
        //    {
        //        var response = await userService.DeleteUser(userDetails);
        //        logger.LogInformation($"User with id ${userDetails.Id} deleted");
        //        return Ok(response);
        //    }
        //    catch (UserNotFoundException)
        //    {
        //        logger.LogError($"User for id {userDetails.Id} not found");
        //        return BadRequest(new ErrorModel("User not found", StatusCodes.Status400BadRequest));
        //    }
        //    catch (PassengerNotFoundException)
        //    {
        //        logger.LogError($"Passenger for user id {userDetails.Id} not found");
        //        return BadRequest(new ErrorModel("Passenger not found", StatusCodes.Status400BadRequest));
        //    }
        //    catch (DriverNotFoundException)
        //    {
        //        logger.LogError($"Driver for user id {userDetails.Id} not found");
        //        return BadRequest(new ErrorModel("Driver not found", StatusCodes.Status400BadRequest));
        //    }
        //    catch (Exception e)
        //    {
        //        logger.LogError(e.Message, e.StackTrace);
        //        return StatusCode(500);
        //    }
        //}
    }
}
