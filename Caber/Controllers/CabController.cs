using Caber.Models;
using Caber.Models.DTOs;
using Caber.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Caber.Controllers
{
    [ExcludeFromCodeCoverage]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class CabController(ICabService cabService, IRoleService roleService) : Controller
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

        [Authorize(Policy = "Driver")]
        [HttpPut("update-location")]
        [ProducesResponseType(typeof(UpdateCabLocationReqsponseDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<UpdateCabLocationReqsponseDto>> UpdateCabLocation([FromBody] UpdateCabLocationRequestDto request)
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
                    return Unauthorized(new ErrorModel("No Access to Resource", StatusCodes.Status401Unauthorized));

                }

                var updatedCab = await cabService.UpdateCabLocation(request.CabId, request.Location);

#pragma warning disable CS8601
                return Ok(new UpdateCabLocationReqsponseDto()
                {
                    Location = updatedCab.Location,
                    CabId = updatedCab.Id,
                    Status = updatedCab.Status.ToString()
                });
#pragma warning restore CS8601
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

    }
}

