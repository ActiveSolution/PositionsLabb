using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PositionsLabb.Data;

namespace PositionsLabb.Api.Controllers
{
    [Route("api/car")]
    [ApiController]
    public class CarController : ControllerBase
    {
        [HttpGet("{id}/positions")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpPost("{id}/positions")]
        public async Task Post(int id, PositionData data)
        {           
            using (var context = new DataContext())
            {
                Vehicle vehicle = await context.Vehicles.FirstOrDefaultAsync(v => v.Id == id);
                if (vehicle == null)
                {
                    vehicle = new Vehicle();
                    await context.Vehicles.AddAsync(vehicle);
                }
                await context.Positions.AddAsync(new Position
                {
                    Latitude = data.Latitude,
                    Longitude = data.Longitude,
                    Vehicle = vehicle
                });
                await context.SaveChangesAsync();
            }
        }
    }

    public class PositionData
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}