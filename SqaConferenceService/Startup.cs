using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Nancy.Owin;
using Serilog;

namespace SqaConferenceService
{
    /// /// <summary>
    /// This class is used by ASP.NET Core apps to configure request processing pipeline and different services. 
    /// </summary>
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseOwin(x =>
                x.UseNancy(options => options.Bootstrapper = new SqaConferenceServiceBootstrapper()));
            loggerFactory.AddConsole();
        }
    }
}
