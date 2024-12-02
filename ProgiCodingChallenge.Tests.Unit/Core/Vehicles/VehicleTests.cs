using FluentAssertions;
using ProgiCodingChallenge.Core;
using ProgiCodingChallenge.Core.VehicleFees;
using ProgiCodingChallenge.Core.Vehicles;
using Xunit;

namespace ProgiCodingChallenge.Tests.Unit.Core.Vehicles;

public class VehicleTests
{
    [Fact]
    public void CalculateTotalPrice_ShouldSetTotalPrice_WhenVehiclePriceIsValid()
    {
        // Arrange
        var price = 1000.00M;
        var vehicle = new TestVehicle();
        vehicle.SetPrice(price);

        // Act
        vehicle.CalculateTotalPrice();

        // Assert
        var expectedTotalFees = 100.00M + 10.00M;
        var expectedTotalPrice = price + expectedTotalFees;

        vehicle.TotalPrice.Should().Be(expectedTotalPrice);
        vehicle.Price.Should().Be(price);
        vehicle.Fees.Should().HaveCount(2);
    }

    [Fact]
    public void SetPrice_ShouldSetPrice_WhenPriceIsValid()
    {
        // Arrange
        var vehicle = new TestVehicle();
        var price = 500.00M;

        // Act
        vehicle.SetPrice(price);

        // Assert
        vehicle.Price.Should().Be(price);
    }

    [Fact]
    public void SetPrice_ShouldThrowArgumentException_WhenPriceIsZeroOrNegative()
    {
        // Arrange
        var vehicle = new TestVehicle();

        // Act
        Action setZeroPrice = () => vehicle.SetPrice(0);
        Action setNegativePrice = () => vehicle.SetPrice(-100);

        // Assert
        setZeroPrice.Should().Throw<ArgumentException>()
            .WithMessage("Vehicle price must be greater than 0.00");

        setNegativePrice.Should().Throw<ArgumentException>()
            .WithMessage("Vehicle price must be greater than 0.00");
    }

    [Fact]
    public void CalculateTotalPrice_ShouldHandleFeesCorrectly_WhenNoFeesExist()
    {
        // Arrange
        var price = 10.00M;
        var vehicle = new TestVehicle();
        vehicle.SetPrice(price);

        // Act
        vehicle.CalculateTotalPrice();

        // Assert
        var expectedTotalFees = vehicle.Fees.Sum(f => f.Value);
        var expectedTotalPrice = price + expectedTotalFees;

        vehicle.TotalPrice.Should().Be(expectedTotalPrice);
        vehicle.Fees.Should().NotBeEmpty();
    }
}

public class TestVehicle : Vehicle
{
    public override VehicleType VehicleType => VehicleType.Common;

    protected override IList<IVehicleFee> CreateFees()
    {
        return
        [
            new StorageVehicleFee(),
            new AssociationVehicleFee(1000)
        ];
    }
}
