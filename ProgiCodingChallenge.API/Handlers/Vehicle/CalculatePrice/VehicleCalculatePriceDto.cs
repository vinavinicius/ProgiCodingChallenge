namespace ProgiCodingChallenge.API.Handlers.Vehicle.CalculatePrice;

public class VehicleCalculatePriceDto
{
    public decimal Price { get; set; }
    public string Type { get; set; } = string.Empty;
    public IList<VehicleFeeDto>? Fees { get; set; }
    public decimal TotalPrice { get; set; }  
}

public class VehicleFeeDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Value { get; set; }
}