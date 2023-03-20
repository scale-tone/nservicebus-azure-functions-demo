using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using NServiceBus;

[assembly: FunctionsStartup(typeof(Startup))]

[assembly: NServiceBusTriggerFunction("Sales")]
// Uncomment this to enable SendsAtomicWithReceive aka CrossEntityTransactions. IMPORTANT: this also requires extensions.ServiceBus.EnableCrossEntityTransactions = true in host.json.
// [assembly: NServiceBusTriggerFunction("Sales", SendsAtomicWithReceive = true)]

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.UseNServiceBus(config => {

            // Trying to force NSB to use built-in deadletter queue instead of custom 'error' queue.
            // This does not work though (results in an infinite loop).
            // config.DoNotSendMessagesToErrorQueue();

            // Configuring custom delayed retry policy - two retries, first after 3 seconds, second after 6 seconds
            var recoverability = config.AdvancedConfiguration.Recoverability();
            recoverability.Delayed(delayed =>
            {
                delayed.NumberOfRetries(2);
                delayed.TimeIncrease(TimeSpan.FromSeconds(3));
            });            

        });
    }
}
