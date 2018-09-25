using Microsoft.EntityFrameworkCore;
using PositionsLabb.Data.Entities;

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
}
