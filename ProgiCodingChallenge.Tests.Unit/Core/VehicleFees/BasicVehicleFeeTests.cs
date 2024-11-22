using FluentAssertions;
using ProgiCodingChallenge.Core;
using ProgiCodingChallenge.Core.VehicleFees;
using Xunit;

namespace ProgiCodingChallenge.Tests.Unit.Core.VehicleFees;

public class BasicVehicleFeeTests
{
    [Fact]
    public void CalculateFee_ShouldReturnCorrectFee_WhenCommonVehicleTypeAndValidPriceProvided()
    {
        // Arrange
        var vehicleType = VehicleType.Common;
        var vehiclePrice = 500.00M;
        var expectedFee = 50.00M;

        // Act
        var vehicleFee = new BasicVehicleFee(vehicleType, vehiclePrice);

        // Assert
        vehicleFee.Value.Should().Be(expectedFee);
    }

    [Fact]
    public void CalculateFee_ShouldReturnCorrectFee_WhenLuxuryVehicleTypeAndValidPriceProvided()
    {
        // Arrange
        var vehicleType = VehicleType.Luxury;
        var vehiclePrice = 1500.00M;
        var expectedFee = 150.00M;

        // Act
        var vehicleFee = new BasicVehicleFee(vehicleType, vehiclePrice);

        // Assert
        vehicleFee.Value.Should().Be(expectedFee);
    }

    [Fact]
    public void CalculateFee_ShouldReturnClampedFee_WhenFeeExceedsMaximum_ForCommonVehicleType()
    {
        // Arrange
        var vehicleType = VehicleType.Common;
        var vehiclePrice = 1000.00M; 

        // Act
        var vehicleFee = new BasicVehicleFee(vehicleType, vehiclePrice);

        // Assert
        vehicleFee.Value.Should().Be(50.00M);
    }

    [Fact]
    public void CalculateFee_ShouldReturnClampedFee_WhenFeeBelowMinimum_ForLuxuryVehicleType()
    {
        // Arrange
        var vehicleType = VehicleType.Luxury;
        var vehiclePrice = 100.00M;

        // Act
        var vehicleFee = new BasicVehicleFee(vehicleType, vehiclePrice);

        // Assert
        vehicleFee.Value.Should().Be(25.00M);
    }

    [Fact]
    public void CalculateFee_ShouldThrowException_WhenUnsupportedVehicleTypeAndValidPriceProvided()
    {
        // Arrange
        var vehicleType = (VehicleType)999; // Invalid vehicle type
        var vehiclePrice = 500.00M;

        // Act
        Action act = () => new BasicVehicleFee(vehicleType, vehiclePrice);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage($"BasicVehicleFee for the type {vehicleType} is not implemented.");
    }

    [Fact]
    public void CalculateFee_ShouldThrowException_WhenInvalidVehiclePrice()
    {
        // Arrange
        var vehicleType = VehicleType.Common;
        var vehiclePrice = 0M;

        // Act
        Action act = () => new BasicVehicleFee(vehicleType, vehiclePrice);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("VehiclePrice invalid for BasicVehicleFee");
    }
}
