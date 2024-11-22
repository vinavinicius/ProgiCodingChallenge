using FluentAssertions;
using ProgiCodingChallenge.Core;
using ProgiCodingChallenge.Core.Vehicles;
using Xunit;

namespace ProgiCodingChallenge.Tests.Unit.Core.Vehicles;

public class VehicleFactoryTests
{
    private readonly IVehicleFactory _vehicleFactory;

    public VehicleFactoryTests()
    {
        _vehicleFactory = new VehicleFactory();
    }

    [Fact]
    public void CreateVehicle_ShouldReturnCommonVehicle_WhenCommonVehicleTypeProvided()
    {
        // Arrange
        var vehicleType = VehicleType.Common;

        // Act
        var vehicle = _vehicleFactory.CreateVehicle(vehicleType);

        // Assert
        vehicle.Should().BeOfType<CommonVehicle>();
    }

    [Fact]
    public void CreateVehicle_ShouldReturnLuxuryVehicle_WhenLuxuryVehicleTypeProvided()
    {
        // Arrange
        var vehicleType = VehicleType.Luxury;

        // Act
        var vehicle = _vehicleFactory.CreateVehicle(vehicleType);

        // Assert
        vehicle.Should().BeOfType<LuxuryVehicle>();
    }

    [Fact]
    public void CreateVehicle_ShouldThrowNotImplementedException_WhenUnknownVehicleTypeProvided()
    {
        // Arrange
        var vehicleType = (VehicleType)999;

        // Act
        Action act = () => _vehicleFactory.CreateVehicle(vehicleType);

        // Assert
        act.Should().Throw<NotImplementedException>()
           .WithMessage($"VehiclePriceCalculation for the type {vehicleType} not implemented");
    }
}
