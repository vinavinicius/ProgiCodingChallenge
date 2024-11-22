namespace ProgiCodingChallenge.API.Handlers.Vehicle.CalculatePrice;

public class VehicleCalculatePriceCommand
{
    public required string VehicleType { get; set; }
    public required decimal VehiclePrice { get; set; }
}
