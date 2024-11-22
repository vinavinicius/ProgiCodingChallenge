namespace ProgiCodingChallenge.Core.VehicleFees;

public sealed class EspecialVehicleFee : IVehicleFee
{
    private static readonly Dictionary<VehicleType, decimal> SupportedVehicleTypeFee = new()
    {
        { VehicleType.Luxury, 0.04M },
        { VehicleType.Common, 0.02M }
    };

    private readonly VehicleType _vehicleType;
    private readonly decimal _vehiclePrice;

    public EspecialVehicleFee(VehicleType vehicleType, decimal vehiclePrice)
    {
        if (!SupportedVehicleTypeFee.ContainsKey(vehicleType))
            throw new ArgumentException($"EspecialVehicleFee for the type {vehicleType} is not implemented.");

        if (vehiclePrice <= 0)
            throw new ArgumentException("VehiclePrice invalid for EspecialVehicleFee");

        _vehicleType = vehicleType;
        _vehiclePrice = vehiclePrice;
    }


    public string Name => "Especial Fee";

    public decimal Value => CalculateFee();

    private decimal CalculateFee()
    {
        var fee = SupportedVehicleTypeFee[_vehicleType];

        var calculatedFee = _vehiclePrice * fee;

        return calculatedFee;
    }
}
