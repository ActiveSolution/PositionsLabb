using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace PositionsLabb.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
            : this(new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PositionsLabb;Trusted_Connection=True;")
                .Options)
        {
            
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }
        public DbSet<City> Cities { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle{ Id = 1 },
                new Vehicle{ Id = 2 },
                new Vehicle{ Id = 3 },
                new Vehicle{ Id = 4 },
                new Vehicle{ Id = 5 },
                new Vehicle{ Id = 6 },
                new Vehicle{ Id = 7 }
            );
        }
    }

    public abstract class Identity
    {
        public int Id { get; set; }
    }

    public class City : Identity
    {
        public string Name { get; set; }
    }

    public class Position : Identity
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Vehicle Vehicle { get; set; } 
    }

    public class Vehicle : Identity
    {
        public int Mileage { get; set; }
    }
}
