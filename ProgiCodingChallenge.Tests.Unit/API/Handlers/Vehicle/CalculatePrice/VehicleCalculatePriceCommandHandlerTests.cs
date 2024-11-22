using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using ProgiCodingChallenge.API.Handlers.Vehicle.CalculatePrice;
using ProgiCodingChallenge.Core;
using ProgiCodingChallenge.Core.Vehicles;
using Xunit;

namespace ProgiCodingChallenge.Tests.Unit.API.Handlers.Vehicle.CalculatePrice;

public class VehicleCalculatePriceCommandHandlerTests
{
    private readonly IVehicleFactory _vehicleFactoryFake = A.Fake<IVehicleFactory>();
    private readonly IValidator<VehicleCalculatePriceCommand> _validatorFake = A.Fake<IValidator<VehicleCalculatePriceCommand>>();
    private readonly VehicleCalculatePriceCommandHandler _handler;

    public VehicleCalculatePriceCommandHandlerTests()
    {
        _handler = new VehicleCalculatePriceCommandHandler(_vehicleFactoryFake, _validatorFake);
    }

    [Fact]
    public void Handle_ShouldReturnError_WhenCommandIsInvalid()
    {
        // Arrange
        var invalidCommand = new VehicleCalculatePriceCommand { VehicleType = "", VehiclePrice = -1 };
        var validationResult = new ValidationResult(new[]
        {
            new ValidationFailure("VehicleType", "Vehicle type is required"),
            new ValidationFailure("VehiclePrice", "Price must be greater than 0")
        });

        A.CallTo(() => _validatorFake.Validate(invalidCommand)).Returns(validationResult);

        // Act
        var result = _handler.Handle(invalidCommand);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be("BadRequest");
    }

    [Fact]
    public void Handle_ShouldReturnDto_WhenCommandIsValid()
    {
        // Arrange
        var validCommand = new VehicleCalculatePriceCommand { VehicleType = "Luxury", VehiclePrice = 100m };
        var validationResult = new ValidationResult();
        var vehicle = new LuxuryVehicle();
        vehicle.SetPrice(100M);

        A.CallTo(() => _validatorFake.Validate(validCommand)).Returns(validationResult);

        A.CallTo(() => _vehicleFactoryFake.CreateVehicle(VehicleType.Luxury)).Returns(vehicle);

        // Act
        var result = _handler.Handle(validCommand);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().NotBeNull();
        result.Value.Type.Should().Be("Luxury");
        result.Value.TotalPrice.Should().Be(234M);
        result.Value.Price.Should().Be(100M);
        result.Value.Fees.Should().HaveCount(4);
    }

    [Fact]
    public void Handle_ShouldReturnError_WhenFactoryThrowsException()
    {
        // Arrange
        var validCommand = new VehicleCalculatePriceCommand { VehicleType = "Luxury", VehiclePrice = 100m };
        var validationResult = new ValidationResult();

        A.CallTo(() => _validatorFake.Validate(validCommand)).Returns(validationResult);

        A.CallTo(() => _vehicleFactoryFake.CreateVehicle(VehicleType.Luxury))
            .Throws(new Exception("Factory error"));

        // Act
        var result = _handler.Handle(validCommand);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Description.Should().Contain("Error: Factory error");
    }
}