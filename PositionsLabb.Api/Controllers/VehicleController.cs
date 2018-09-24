using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PositionsLabb.Data;

namespace PositionsLabb.Api.Controllers
{
    [Route("api/vehicle")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        [HttpGet("{id}/positions/{date}")]
        public Position[] Get(Guid vehicleId, DateTime date)
        {
            using (var context = new DataContext())
            {
                Position[] vehicleData = context.Positions
                    .Include(p => p.Vehicle)
                    .OrderBy(p => p.DateTimeUtc)
                    .ToArray()
                    .Where(p => p.Vehicle.VehicleId == vehicleId)
                    .Where(p => p.DateTimeUtc == date)
                    .ToArray();

                return vehicleData;
            }
        }

        [HttpPost("positions")]
        public async Task Post(PositionData[] data)
        {
            foreach (PositionData positionData in data)
            {
                using (var context = new DataContext())
                {
                    await context.Positions.AddAsync(new Position
                    {
                        Latitude = positionData.Latitude,
                        Longitude = positionData.Longitude,
                        Vehicle = await context.Vehicles.FirstOrDefaultAsync(v => v.VehicleId == positionData.VehicleId),
                        DateTimeUtc = DateTime.UtcNow
                    });
                    await context.SaveChangesAsync();
                }
            }
        }

    }

    public class PositionData
    {
        public Guid VehicleId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}