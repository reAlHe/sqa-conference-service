using System.IO;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace SqaConferenceService
{
    /// <summary>
    /// Main entry class of the application.
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("Starting...");

            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseSerilog()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
