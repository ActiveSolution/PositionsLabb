using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PositionsLabb.Data
{
    public class Position : Identity
    {
        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; } 
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime DateTimeUtc { get; set; }
    }
}