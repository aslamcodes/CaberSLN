﻿using Caber.Models;
using Microsoft.AspNetCore.Mvc;

namespace Caber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabController(ICabService cabService) : Controller
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
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

    }
}
