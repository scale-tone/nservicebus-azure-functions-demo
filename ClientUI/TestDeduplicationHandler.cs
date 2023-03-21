using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace ClientUI
{    
    public class TestDeduplicationHandler :
        IHandleMessages<TestDedup>
    {
        static readonly ILog log = LogManager.GetLogger<TestDeduplicationHandler>();

        public async Task Handle(TestDedup message, IMessageHandlerContext context)
        {
            log.Info($"Received command with id {message.OrderId}");

            var sendOptions = new SendOptions();
            // This queue should be created with 'Enable duplicate detection' flag set
            sendOptions.SetDestination("dedup");

            // Explicitly setting native message's MessageId to enable deduplication
            sendOptions.CustomizeNativeMessage(m => m.MessageId = message.OrderId);

            // Sending two messages, but expect only one to appear
            await context.Send(message, sendOptions);
            await context.Send(message, sendOptions);
        }
    }
}
