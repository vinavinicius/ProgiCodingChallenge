using ProgiCodingChallenge.Core.VehicleFees;

namespace ProgiCodingChallenge.Core.Vehicles;

public class CommonVehicle : Vehicle
{
    public override VehicleType VehicleType => VehicleType.Common;

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
