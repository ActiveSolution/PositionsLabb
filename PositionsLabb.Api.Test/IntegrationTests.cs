using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using PositionsLabb.Api.Controllers;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;

namespace PositionsLabb.Api.Test
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly TestServer _server;
        private readonly string _vehicleIdsFilePath = Path.Combine(
            Environment.CurrentDirectory, @"..\..\..\..",
            @"PositionsLabb.Api\SeedData\vehicleIds.txt");
            
        public IntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Test_GET()
        {
            Guid vehicleId = Guid.Parse("{39e7b3af-3d0e-4c1e-b7de-4c72b8eecdeb}");
            DateTime dateTimeUtc = DateTime.Parse("2018-09-23");

            HttpClient httpClient = _factory.CreateClient();
            Stopwatch stopwatch = Stopwatch.StartNew();
            
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"/api/vehicles/{vehicleId}/positions/{dateTimeUtc}");
            httpResponseMessage.EnsureSuccessStatusCode();

            stopwatch.Stop();
            TimeSpan time = stopwatch.Elapsed.Duration();
        }

        [Fact]
        public async Task Test_POST()
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            Guid[] ids = Newtonsoft.Json.JsonConvert.DeserializeObject<Guid[]>(File.ReadAllText(_vehicleIdsFilePath));

            PositionData[] positionData = ids
                .Take(200)
                .SelectMany(guid => Enumerable.Range(0, 60*8)
                    .Select(i => new PositionData
                    {
                        VehicleId = guid,
                        Longitude = (rnd.NextDouble() - 0.5) * 90,
                        Latitude = (rnd.NextDouble() - 0.5) * 90,
                        DateTimeUtc = (DateTime.Today.AddDays(-1) + new TimeSpan(8,0,0)).AddMinutes(i)
                    }))
                .ToArray();

            HttpClient client = _factory.CreateClient();
            
            var content = new StringContent(JsonConvert.SerializeObject(positionData), Encoding.UTF8, "application/json");
            Stopwatch stopwatch = Stopwatch.StartNew();
            await client.PostAsync($"/api/vehicles/positions", content);
            stopwatch.Stop();
            TimeSpan time = stopwatch.Elapsed.Duration();
        }
    }
}
