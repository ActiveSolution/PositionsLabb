using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PositionsLabb.Data;

namespace PositionsLabb.Api.Controllers
{
    [Route("api/vehicles")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        [HttpGet("{vehicleId}/positions/{date}")]
        public Position[] Get(Guid vehicleId, DateTime date)
        {
            using (var context = new DataContext())
            {
                Position[] vehicleData = context.Positions
                    .Include(p => p.Vehicle)
                    .OrderBy(p => p.DateTimeUtc)
                    .ToArray()
                    .Where(p => p.Vehicle.VehicleId == vehicleId)
                    .Where(p => p.DateTimeUtc.Date == date.Date)
                    .ToArray();

                return vehicleData;
            }
        }

        [HttpPost("positions")]
        public async Task Post(PositionData[] data)
        {
            foreach (PositionData positionData in data)
            {
                await UpdatePosition(positionData);
            }
        }

        private async Task UpdatePosition(PositionData positionData)
        {
            using (var context = new DataContext())
            {
                await context.Positions.AddAsync(new Position
                {
                    Latitude = positionData.Latitude,
                    Longitude = positionData.Longitude,
                    Vehicle = await context.Vehicles.FirstOrDefaultAsync(v => v.VehicleId == positionData.VehicleId),
                    DateTimeUtc = positionData.DateTimeUtc
                });
                await context.SaveChangesAsync();
            }
        }
    }

    public class PositionData
    {
        public Guid VehicleId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime DateTimeUtc { get; set;}
    }
}