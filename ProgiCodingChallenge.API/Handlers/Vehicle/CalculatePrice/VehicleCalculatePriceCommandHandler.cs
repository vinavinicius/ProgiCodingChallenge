using ErrorOr;
using FluentValidation;
using ProgiCodingChallenge.Core;
using ProgiCodingChallenge.Core.Vehicles;
using System.Net;

namespace ProgiCodingChallenge.API.Handlers.Vehicle.CalculatePrice;

public interface IVehicleCalculatePriceCommandHandler
{
    ErrorOr<VehicleCalculatePriceDto> Handle(VehicleCalculatePriceCommand command);
}

public class VehicleCalculatePriceCommandHandler(
    IVehicleFactory vehicleFactory, 
    IValidator<VehicleCalculatePriceCommand> validator) : IVehicleCalculatePriceCommandHandler
{
    public ErrorOr<VehicleCalculatePriceDto> Handle(VehicleCalculatePriceCommand command)
    {
        var validatorResult = validator.Validate(command);

        if (!validatorResult.IsValid)
        {
            return Error.Unexpected(code: HttpStatusCode.BadRequest.ToString());
        }

        var vehicleType = Enum.Parse<VehicleType>(command.VehicleType);

        try
        {
            var vehicle = vehicleFactory.CreateVehicle(vehicleType);
            vehicle.SetPrice(command.VehiclePrice);
            vehicle.CalculateTotalPrice();

            var vehicleCalculatePriceDto = new VehicleCalculatePriceDto()
            {
                Type = vehicle.VehicleType.ToString(),
                TotalPrice = vehicle.TotalPrice,
                Price = vehicle.Price,
                Fees = vehicle.Fees
                              .Select(fee => new VehicleFeeDto()
                              {
                                  Name = fee.Name,
                                  Value = fee.Value
                              })
                              .ToArray(),
            };

            return vehicleCalculatePriceDto;
        }
        catch (Exception ex)
        {
            return Error.Unexpected(description: $"Error: {ex.Message}");
        }
    }
}