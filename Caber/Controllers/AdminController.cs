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
    public class AdminController(IAdminService adminService) : Controller
    {
        [HttpPut("verify-driver")]
        [ProducesResponseType(typeof(VerifyDriverResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Task<VerifyDriverResponseDto>>> VerifyDriver([FromBody] VerifyDriverRequestDto request)
        {
            try
            {
                return Ok(await adminService.VerifyDriver(request));
            }
            catch (DriverNotFoundException e)
            {
                return NotFound(new ErrorModel(e.Message, StatusCodes.Status404NotFound));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
