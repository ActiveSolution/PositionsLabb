using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PositionsLabb.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options = null)
            : base(options ?? new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PositionsLabb;Trusted_Connection=True;")
                .Options)
        { }

        public DbSet<Position> Positions { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
    }

    public abstract class Identity
    {
        public int Id { get; set; }
    }

    public class Position : Identity
    {
        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; } 
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime DateTimeUtc { get; set; }
    }

    public class Vehicle : Identity
    {
        public Guid VehicleId { get; set; }
        public int Mileage { get; set; }
    }
}
