using Messages;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using NServiceBus;

[assembly: FunctionsStartup(typeof(Startup))]
[assembly: NServiceBusTriggerFunction("ClientUI")]

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.UseNServiceBus(config => {

            var routing = config.Routing;

            routing.RouteToEndpoint(typeof(PlaceOrder), "Sales");
        });
    }
}
