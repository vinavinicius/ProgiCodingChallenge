using FluentAssertions;
using Xunit;
using ProgiCodingChallenge.Core.VehicleFees;

namespace ProgiCodingChallenge.Tests.Unit.Core.VehicleFees;

public class StorageVehicleFeeTests
{
    [Fact]
    public void Name_ShouldReturnStorage_WhenCalled()
    {
        // Arrange
        var vehicleFee = new StorageVehicleFee();

        // Act
        var name = vehicleFee.Name;

        // Assert
        name.Should().Be("Storage Fee");
    }

    [Fact]
    public void Value_ShouldReturn100_WhenCalled()
    {
        // Arrange
        var vehicleFee = new StorageVehicleFee();

        // Act
        var value = vehicleFee.Value;

        // Assert
        value.Should().Be(100.00M);
    }
}
