using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RocketCoordinateCheckerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RocketController : ControllerBase
    {        
        private readonly ILogger<RocketController> _logger;

        public RocketController(ILogger<RocketController> logger)
        {
            _logger = logger;
        }

        // OPTIONAL: If NOT executed, default area will be ==>>  10x10 at position 0x0  -------------------
        [HttpGet("ConfigurePlatformSize/{coordinateX}/{coordinateY}/{sizeX}/{sizeY}")]
        public IActionResult ConfigurePlatformSize(int coordinateX, int coordinateY, int sizeX, int sizeY)
        {            
            CorodinateSystem platformCoordinate = new CorodinateSystem();            
            platformCoordinate.ConfigureCoordinates(coordinateX, coordinateY, sizeX, sizeY);

            var response = JsonSerializer.Serialize(platformCoordinate.coordinateConfigurationResult); // Result phrase : "clash"  || "ok for landing"
            if (platformCoordinate.IsConfigValuesAreGood)
            {
                return Ok(response); // return Success 200 with description
            }
            else
            {
                return StatusCode(400, response); // return Bad request 400 with description
            }
        }
        //-------------------------------------------------------------------------------------------------
        
        //(Checks if rpevious data exists or it's a first time inquiry)
        [HttpGet("CheckPreviousRocketInquiry/{coordinateX}/{coordinateY}")]
        public IActionResult CheckPreviousRocketInquiry(int coordinateX, int coordinateY)
        {            
            CorodinateSystem previousRocketCoordinates = new CorodinateSystem();
            previousRocketCoordinates.CheckPreviosRocketCoordinates(coordinateX, coordinateY);
            var response = JsonSerializer.Serialize(previousRocketCoordinates.clashResult); // Result phrase : "clash"  || "ok for landing"
            return Ok(response); // Success return with description
        }
        
        //Check Landing positions:  Can be called independently from Client.
        [HttpGet("CheckLandingAreaAvailabilitiy/{coordinateX}/{coordinateY}")]
        public IActionResult CheckLandingAreaAvailabilitiy(int coordinateX, int coordinateY)
        {
            CorodinateSystem checkLandingAreaAvailabilitiy = new CorodinateSystem();
            checkLandingAreaAvailabilitiy.CheckForLanding(coordinateX, coordinateY);

            var response = JsonSerializer.Serialize(checkLandingAreaAvailabilitiy.landingPositionResult); // Result phrase : "clash"  || "ok for landing"
            return Ok(response); // Success return with description
        }

    }
}
