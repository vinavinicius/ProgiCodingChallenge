using ProgiCodingChallenge.Core.VehicleFees;

namespace ProgiCodingChallenge.Core.Vehicles;

public class LuxuryVehicle : Vehicle
{
    public override VehicleType VehicleType => VehicleType.Luxury;

    protected override IList<IVehicleFee> GetFees()
    {
        return [
            new BasicVehicleFee(VehicleType, Price),
            new EspecialVehicleFee(VehicleType, Price),
            new AssociationVehicleFee(Price),
            new StorageVehicleFee()
        ];
    }
}