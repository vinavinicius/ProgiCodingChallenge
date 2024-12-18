﻿using ProgiCodingChallenge.Core.VehicleFees;

namespace ProgiCodingChallenge.Core.Vehicles;

public abstract class Vehicle
{
    public decimal TotalPrice { get; private set; }
    public decimal Price { get; private set; }
    public IList<IVehicleFee> Fees { get; private set; } = [];

    public void CalculateTotalPrice()
    {
        var totalFees = Fees.Sum(s => s.Value);

        TotalPrice = Price + totalFees;
    }

    public void SetPrice(decimal price)
    {
        if (price <= 0)
        {
            throw new ArgumentException("Vehicle price must be greater than 0.00");
        }

        Price = price;
        Fees = CreateFees();
    }

    public abstract VehicleType VehicleType { get; }

    protected virtual IList<IVehicleFee> CreateFees()
    {
        return [
            new BasicVehicleFee(VehicleType, Price),
            new EspecialVehicleFee(VehicleType, Price),
            new AssociationVehicleFee(Price),
            new StorageVehicleFee()
        ];
    }
}