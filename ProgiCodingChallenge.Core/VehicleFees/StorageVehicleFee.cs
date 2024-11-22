namespace ProgiCodingChallenge.Core.VehicleFees;

public sealed class StorageVehicleFee() : IVehicleFee
{
    public string Name => "Storage Fee";

    public decimal Value => 100.00M;
}
