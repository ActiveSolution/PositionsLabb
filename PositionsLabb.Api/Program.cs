using System;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PositionsLabb.Data;

namespace PositionsLabb.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .Build()
                .EnsureCreated()
                .EnsureSeeded()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }

    public static class WebHostExtensions
    {
        public static IWebHost EnsureCreated(this IWebHost webHost)
        {
            var serviceScopeFactory = (IServiceScopeFactory)webHost.Services.GetService(typeof(IServiceScopeFactory));

            using (IServiceScope scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

                dbContext.Database.Migrate();
            }

            return webHost;
        }

        public static IWebHost EnsureSeeded(this IWebHost webHost)
        {
            var serviceScopeFactory = (IServiceScopeFactory)webHost.Services.GetService(typeof(IServiceScopeFactory));

            using (IServiceScope scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

                if (DataBaseEmpty(dbContext))
                {
                    Seed(dbContext);
                }

                return webHost;
            }

            bool DataBaseEmpty(DataContext dbContext)
            {
                return false == dbContext.Vehicles.AnyAsync().GetAwaiter().GetResult();
            }

            void Seed(DataContext dbContext)
            {
                var rnd = new Random(Guid.NewGuid().GetHashCode());
                Vehicle[] vehicles = Enumerable.Range(0, 29000)
                    .Select(i => new Vehicle
                    {
                        VehicleId = Guid.NewGuid(),
                        Mileage = rnd.Next(100, 100000),
                    })
                    .ToArray();

                dbContext.Vehicles.AddRange(vehicles);
                dbContext.SaveChanges();
            }
        }
    }
}
