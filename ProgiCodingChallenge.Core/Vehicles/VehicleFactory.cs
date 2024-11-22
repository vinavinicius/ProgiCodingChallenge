namespace ProgiCodingChallenge.Core.Vehicles;

public interface IVehicleFactory
{
    Vehicle CreateVehicle(VehicleType vehicleType);
}

public class VehicleFactory : IVehicleFactory
{
    public Vehicle CreateVehicle(VehicleType vehicleType)
    {
        return vehicleType switch
        {
            VehicleType.Common => new CommonVehicle(),
            VehicleType.Luxury => new LuxuryVehicle(),
            _ => throw new NotImplementedException($"VehiclePriceCalculation for the type {vehicleType} not implemented"),
        };
    }
}

