using ExecutionContext = Microsoft.Azure.WebJobs.ExecutionContext;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using NServiceBus;

/// <summary>
/// Custom Service Bus Trigger definition, that enables Sessions
/// </summary>
class FunctionEndpointTrigger
{
    readonly IFunctionEndpoint endpoint;

    public FunctionEndpointTrigger(IFunctionEndpoint endpoint)
    {
        this.endpoint = endpoint;
    }

    [FunctionName("NServiceBusFunctionEndpointTrigger-Wholesale")]
    public Task Run(
        [ServiceBusTrigger(queueName: "Wholesale", AutoCompleteMessages = true, IsSessionsEnabled = true)]
        ServiceBusReceivedMessage message,
        ILogger logger,
        ExecutionContext executionContext,
        CancellationToken cancellationToken)
    {
        return endpoint.ProcessNonAtomic(message, executionContext, logger, cancellationToken);
    }
}