using Microsoft.AspNetCore.Mvc;
using ProgiCodingChallenge.API.Handlers.Vehicle.CalculatePrice;

namespace ProgiCodingChallenge.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehicleController(
        IVehicleCalculatePriceCommandHandler vehicleCalculatePriceCommandHandler
    ) : Controller
{
    [HttpPost("CalculatePrice")]
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

