using System;

namespace PositionsLabb.Api.Models
{
    public class PositionData
    {
        public Guid VehicleId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime DateTimeUtc { get; set;}
    }
}