namespace ProgiCodingChallenge.Core.VehicleFees;

public sealed class BasicVehicleFee : IVehicleFee
{
    private static readonly List<BasicVehicleFeeRule> SupportedVehicleTypeFee =
    [
        new BasicVehicleFeeRule(VehicleType.Common, 10.00M, 50.00M, 0.10M),
        new BasicVehicleFeeRule(VehicleType.Luxury, 25.00M, 200.00M, 0.10M)
    ];

    private readonly VehicleType _vehicleType;
    private readonly decimal _vehiclePrice;

    public BasicVehicleFee(VehicleType vehicleType, decimal vehiclePrice)
    {
        if (!Enum.IsDefined(typeof(VehicleType), vehicleType))
            throw new ArgumentException($"BasicVehicleFee for the type {vehicleType} is not implemented.");

        if (vehiclePrice <= 0)
            throw new ArgumentException("VehiclePrice invalid for BasicVehicleFee");

        _vehicleType = vehicleType;
        _vehiclePrice = vehiclePrice;
    }

    public string Name => "Basic Fee";

    public decimal Value => CalculateFee();

    private decimal CalculateFee()
    {
        var rule = SupportedVehicleTypeFee.Single(f => f.VehicleType == _vehicleType);

        var feeCalculated = _vehiclePrice * rule.FeeRate;

        if (feeCalculated < rule.MinimumFee)
        {
            return rule.MinimumFee;
        }

        if (feeCalculated > rule.MaximumFee)
        {
            return rule.MaximumFee;
        }

        return feeCalculated;
    }
}

public class BasicVehicleFeeRule(VehicleType vehicleType, decimal minimumFee, decimal maximumFee, decimal feeRate)
{
    public VehicleType VehicleType { get; } = vehicleType;
    public decimal MinimumFee { get; } = minimumFee;
    public decimal MaximumFee { get; } = maximumFee;
    public decimal FeeRate { get; } = feeRate;
}


