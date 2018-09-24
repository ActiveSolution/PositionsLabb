using System;

namespace PositionsLabb.Data
{
    public class Vehicle : Identity
    {
        public Guid VehicleId { get; set; }
        public int Mileage { get; set; }
    }
}