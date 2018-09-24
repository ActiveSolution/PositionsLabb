using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

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
}
