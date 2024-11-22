namespace ProgiCodingChallenge.Core.VehicleFees;

public sealed class AssociationVehicleFee(decimal vehiclePrice) : IVehicleFee
{
    private static readonly AssociationFeeRange[] FeeRanges =
    [
        new AssociationFeeRange(1m, 500m, 5m),
        new AssociationFeeRange(501m, 1000m, 10m),
        new AssociationFeeRange(1001m, 3000m, 15m),
        new AssociationFeeRange(3001m, decimal.MaxValue, 20m)
    ];

    public string Name => "Association Fee";

    public decimal Value => CalculateFee();

    private decimal CalculateFee()
    {
        var matchingRange = FeeRanges
            .SingleOrDefault(range => vehiclePrice >= range.MinPrice && vehiclePrice <= range.MaxPrice);

        return matchingRange?.Fee ?? 0;
    }
}

public class AssociationFeeRange(decimal minPrice, decimal maxPrice, decimal fee)
{
    public decimal MinPrice { get; set; } = minPrice;
    public decimal MaxPrice { get; set; } = maxPrice;
    public decimal Fee { get; set; } = fee;
}

