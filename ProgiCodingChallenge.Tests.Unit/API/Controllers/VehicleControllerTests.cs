using ErrorOr;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ProgiCodingChallenge.API.Controllers;
using ProgiCodingChallenge.API.Handlers.Vehicle.CalculatePrice;
using Xunit;

namespace ProgiCodingChallenge.Tests.Unit.API.Controllers;

public class VehicleControllerTests
{
    private readonly IVehicleCalculatePriceCommandHandler _vehicleCalculatePriceCommandHandlerFake;
    private readonly VehicleController _controller;

    public VehicleControllerTests()
    {
        _vehicleCalculatePriceCommandHandlerFake = A.Fake<IVehicleCalculatePriceCommandHandler>();
        _controller = new VehicleController(_vehicleCalculatePriceCommandHandlerFake);
    }

    [Fact]
    public void CalculatePrice_ShouldReturnOk_WhenCommandIsValid()
    {
        // Arrange
        var validCommand = new VehicleCalculatePriceCommand { VehicleType = "Car", VehiclePrice = 100m };
        var vehicleCalculatePriceDto = new VehicleCalculatePriceDto
        {
            Type = "Car",
            TotalPrice = 125m,
            Price = 100m,
            Fees =
            [
                new VehicleFeeDto { Name = "Fee1", Value = 15m }
            ]
        };

        A.CallTo(() => _vehicleCalculatePriceCommandHandlerFake.Handle(validCommand)).Returns(vehicleCalculatePriceDto);

        // Act
        var actionResult = _controller.CalculatePrice(validCommand);

        // Assert
        actionResult.Should().BeOfType<OkObjectResult>();
        var okResult = actionResult as OkObjectResult;
        okResult!.Value.Should().BeEquivalentTo(vehicleCalculatePriceDto);
    }

    [Fact]
    public void CalculatePrice_ShouldReturnBadRequest_WhenCommandIsInvalid()
    {
        // Arrange
        var invalidCommand = new VehicleCalculatePriceCommand { VehicleType = "", VehiclePrice = -1 };
        var result = Error.Unexpected("Invalid command");

        A.CallTo(() => _vehicleCalculatePriceCommandHandlerFake.Handle(invalidCommand)).Returns(result);

        // Act
        var actionResult = _controller.CalculatePrice(invalidCommand);

        // Assert
        actionResult.Should().BeOfType<BadRequestResult>();
    }
}