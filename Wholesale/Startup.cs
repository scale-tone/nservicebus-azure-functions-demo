using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using NServiceBus;

[assembly: FunctionsStartup(typeof(Startup))]

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.UseNServiceBus("Wholesale");
    }
}
