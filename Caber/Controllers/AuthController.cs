using Caber.Exceptions;
using Caber.Models;
using Caber.Models.DTOs;
using Caber.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Caber.Controllers
{
    [EnableCors]
    [ExcludeFromCodeCoverage]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : Controller
    {
        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterRequestDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorModel("Request fields are invalid", StatusCodes.Status400BadRequest));
            }
            try
            {
                AuthResponseDto res = await authService.Register(registerDto);

                return Ok(res);
            }
            catch (CannotRegisterUserException e)
            {
                return BadRequest(new ErrorModel(e.Message, StatusCodes.Status400BadRequest));
            }
            catch
            {
                return StatusCode(500);
            }

        }

        [HttpPost("register-passenger")]
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<AuthResponseDto>> RegisterPassenger([FromBody] RegisterPassengerRequestDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorModel("Request fields are invalid", StatusCodes.Status400BadRequest));
            }
            try
            {
                AuthResponseDto res = await authService.RegisterPassenger(registerDto);

                return Ok(res);
            }
            catch (CannotRegisterUserException e)
            {
                return BadRequest(new ErrorModel(e.Message, StatusCodes.Status400BadRequest));
            }
            catch
            {
                return StatusCode(500);
            }

        }


        [HttpPost("register-driver")]
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<AuthResponseDto>> RegisterDriver([FromBody] RegisterDriverRequestDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorModel("Request fields are invalid", StatusCodes.Status400BadRequest));
            }
            try
            {
                AuthResponseDto res = await authService.RegisterDriver(registerDto);

                return Ok(res);
            }
            catch (CannotRegisterUserException e)
            {
                return BadRequest(new ErrorModel(e.Message, StatusCodes.Status400BadRequest));
            }
            catch
            {
                return StatusCode(500);
            }

        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginRequestDto userLogin)
        {
            try
            {
                return await authService.Login(userLogin);
            }

            catch (UnauthorizedUserException)
            {
                return Unauthorized(new ErrorModel("Invalid Credentials", StatusCodes.Status401Unauthorized));
            }
            catch (UserNotFoundException)
            {
                return Unauthorized(new ErrorModel("Invalid Credentials", StatusCodes.Status401Unauthorized));
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }


}
