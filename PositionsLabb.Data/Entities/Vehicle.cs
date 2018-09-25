using System;

namespace PositionsLabb.Data.Entities
{
    public class Vehicle : Identity
    {
        public Guid VehicleId { get; set; }
        public int Mileage { get; set; }
    }
}