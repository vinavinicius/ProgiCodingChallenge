using FluentAssertions;
using ProgiCodingChallenge.Core;
using ProgiCodingChallenge.Core.VehicleFees;
using Xunit;

namespace ProgiCodingChallenge.Tests.Unit.Core.VehicleFees;

public class EspecialVehicleFeeTests
{
    [Fact]
    public void CalculateFee_ShouldReturnCalculatedFee_WhenLuxuryVehicleTypeAndValidPriceProvided()
    {
        // Arrange
        var vehicleType = VehicleType.Luxury;
        var vehiclePrice = 1000.00M;
        var expectedFee = 40.00M;

        // Act
        var vehicleFee = new EspecialVehicleFee(vehicleType, vehiclePrice);

        // Assert
        vehicleFee.Value.Should().Be(expectedFee);
    }

    [Fact]
    public void CalculateFee_ShouldReturnCalculatedFee_WhenCommonVehicleTypeAndValidPriceProvided()
    {
        // Arrange
        var vehicleType = VehicleType.Common;
        var vehiclePrice = 1000.00M;
        var expectedFee = 20.00M;

        // Act
        var vehicleFee = new EspecialVehicleFee(vehicleType, vehiclePrice);

        // Assert
        vehicleFee.Value.Should().Be(expectedFee);
    }


    [Fact]
    public void CalculateFee_ShouldThrowException_WhenUnsupportedVehicleTypeAndValidPriceProvided()
    {
        // Arrange
        var vehicleType = (VehicleType)999; // Invalid vehicle type
        var vehiclePrice = 500.00M;

        // Act
        Action act = () => new EspecialVehicleFee(vehicleType, vehiclePrice);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage($"EspecialVehicleFee for the type {vehicleType} is not implemented.");
    }

    [Fact]
    public void CalculateFee_ShouldThrowException_WhenInvalidVehiclePrice()
    {
        // Arrange
        var vehicleType = VehicleType.Common;
        var vehiclePrice = 0M;

        // Act
        Action act = () => new EspecialVehicleFee(vehicleType, vehiclePrice);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("VehiclePrice invalid for EspecialVehicleFee");
    }
}
