using FluentAssertions;
using ProgiCodingChallenge.Core.VehicleFees;
using Xunit;

namespace ProgiCodingChallenge.Tests.Unit.Core.VehicleFees;

public class AssociationVehicleFeeTests
{
    [Fact]
    public void CalculateFee_ShouldReturnCorrectFee_WhenPriceFallsBetween_1_And_500Range()
    {
        // Arrange
        var vehiclePrice = 300.00M;
        var expectedFee = 5.00M;

        // Act
        var vehicleFee = new AssociationVehicleFee(vehiclePrice);

        // Assert
        vehicleFee.Value.Should().Be(expectedFee);
    }

    [Fact]
    public void CalculateFee_ShouldReturnCorrectFee_WhenPriceFallsBetween_501_And_1000Range()
    {
        // Arrange
        var vehiclePrice = 750.00M;
        var expectedFee = 10.00M;

        // Act
        var vehicleFee = new AssociationVehicleFee(vehiclePrice);

        // Assert
        vehicleFee.Value.Should().Be(expectedFee);
    }

    [Fact]
    public void CalculateFee_ShouldReturnCorrectFee_WhenPriceFallsBetween_1001_And_3000Range()
    {
        // Arrange
        var vehiclePrice = 2000.00M;
        var expectedFee = 15.00M;

        // Act
        var vehicleFee = new AssociationVehicleFee(vehiclePrice);

        // Assert
        vehicleFee.Value.Should().Be(expectedFee);
    }

    [Fact]
    public void CalculateFee_ShouldReturnCorrectFee_WhenPriceIsGreaterThan_3001()
    {
        // Arrange
        var vehiclePrice = 4000.00M;
        var expectedFee = 20.00M;

        // Act
        var vehicleFee = new AssociationVehicleFee(vehiclePrice);

        // Assert
        vehicleFee.Value.Should().Be(expectedFee);
    }

    [Fact]
    public void CalculateFee_ShouldReturnZero_WhenPriceIsBelowMinPriceRange()
    {
        // Arrange
        var vehiclePrice = 0.00M;
        var expectedFee = 0.00M;

        // Act
        var vehicleFee = new AssociationVehicleFee(vehiclePrice);

        // Assert
        vehicleFee.Value.Should().Be(expectedFee);
    }
}
