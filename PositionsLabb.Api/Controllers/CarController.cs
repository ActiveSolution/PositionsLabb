using System.Linq;
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
        public async Task Post(int id, PositionData[] data)
        {
            foreach (PositionData positionData in data)
            {
                using (var context = new DataContext())
                {
                    await context.Positions.AddAsync(new Position
                    {
                        Latitude = positionData.Latitude,
                        Longitude = positionData.Longitude,
                        Vehicle = await context.Vehicles.FirstOrDefaultAsync(v => v.Id == id)
                    });
                    await context.SaveChangesAsync();
                }
            }
        }

    }

    public class PositionData
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}