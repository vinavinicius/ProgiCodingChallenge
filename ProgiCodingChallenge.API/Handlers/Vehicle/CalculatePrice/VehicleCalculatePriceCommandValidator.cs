using FluentValidation;
using ProgiCodingChallenge.Core;

namespace ProgiCodingChallenge.API.Handlers.Vehicle.CalculatePrice;

public class VehicleCalculatePriceCommandValidator : AbstractValidator<VehicleCalculatePriceCommand>
{
    public VehicleCalculatePriceCommandValidator()
    {
        RuleFor(x => x.VehiclePrice)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.VehicleType)
            .Must(i => Enum.IsDefined(typeof(VehicleType), i));
    }
}
       
