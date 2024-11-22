using Microsoft.AspNetCore.Mvc;
using ProgiCodingChallenge.API.Handlers.Vehicle.CalculatePrice;

namespace ProgiCodingChallenge.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/{v:apiVersion}/[controller]")]
public class VehicleController(
        IVehicleCalculatePriceCommandHandler vehicleCalculatePriceCommandHandler
    ) : Controller
{
    [HttpPost("CalculatePrice")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CalculatePrice([FromBody] VehicleCalculatePriceCommand calculatePriceCommand)
    {
        var result = vehicleCalculatePriceCommandHandler.Handle(calculatePriceCommand);

        var actionResult = result.Match<IActionResult>(
            value => Ok(value) ,
            (errors) => BadRequest()
        );

        return actionResult;
    }
}

