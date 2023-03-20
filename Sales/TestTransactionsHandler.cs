using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Sales
{    
    public class TestTransactionsHandler :
        IHandleMessages<TestTransactions>
    {
        static readonly ILog log = LogManager.GetLogger<TestTransactionsHandler>();

        public async Task Handle(TestTransactions message, IMessageHandlerContext context)
        {
            log.Info($"Received command with id {message.OrderId}");

            var billingOptions = new SendOptions();
            billingOptions.SetDestination("Billing");

            await context.Send(message, billingOptions);

            var shippingOptions = new SendOptions();
            shippingOptions.SetDestination("Chipping");

            await context.Send(message, shippingOptions);
        }
    }
}
