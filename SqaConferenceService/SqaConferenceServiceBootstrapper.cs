using Nancy;
using Nancy.Bootstrapper;
using Nancy.Configuration;
using Nancy.TinyIoc;

namespace SqaConferenceService
{
    /// <inheritdoc />
    /// <summary>
    /// This class customizes the runtime behaviour of Nancy to register.
    /// </summary>
    public class SqaConferenceServiceBootstrapper : DefaultNancyBootstrapper
    {
        public override void Configure(INancyEnvironment environment)
        {
            var config = new TraceConfiguration(true, true);
            environment.AddValue(config);
        }
        
        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            pipelines.AfterRequest.AddItemToEndOfPipeline(ctx =>
            {
                ctx.Response.WithHeader("Access-Control-Allow-Origin", "*")
                    .WithHeader("Access-Control-Allow-Methods", "POST, GET, PUT, DELETE, OPTIONS")
                    .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type")
                    .WithHeader("Allow", "POST, GET, DELETE, PUT, OPTIONS");
            });
            base.RequestStartup(container, pipelines, context);
        }
    }
}
