using System;
using System.Collections.Generic;

namespace Domain.Entity.Vehicles;
public partial class Vehicle
{
    public int VehicleId { get; set; }
    public int Year { get; set; }
    public string Brand { get; set; } = null!;
    public string Model { get; set; } = null!;
    public int Price { get; set; }
    public string Condition { get; set; } = null!;
    public string? Motor { get; set; }
    public string Type { get; set; } = null!;
    public int Seats { get; set; }
    public string? ExteriorColor { get; set; }
    public string? InteriorColor { get; set; }
    public string FuelType { get; set; } = null!;
    public string Transmission { get; set; } = null!;
    public int Mileage { get; set; }
}