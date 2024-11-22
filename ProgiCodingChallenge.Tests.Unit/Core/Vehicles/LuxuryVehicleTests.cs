using FluentAssertions;
using ProgiCodingChallenge.Core.VehicleFees;
using ProgiCodingChallenge.Core.Vehicles;
using Xunit;

namespace ProgiCodingChallenge.Tests.Unit.Core.Vehicles;

public class LuxuryVehicleTests
{
    [Fact]
    public void GetFees_ShouldReturnAllFees_WhenCalled()
    {
        // Arrange
        var vehicle = new LuxuryVehicle();
        vehicle.SetPrice(10.00M);

        // Act
        var fees = vehicle.Fees;

        // Assert
        fees.Should().HaveCount(4);
        fees[0].Should().BeOfType<BasicVehicleFee>(); 
        fees[1].Should().BeOfType<EspecialVehicleFee>();
        fees[2].Should().BeOfType<AssociationVehicleFee>();
        fees[3].Should().BeOfType<StorageVehicleFee>();
    }
}
