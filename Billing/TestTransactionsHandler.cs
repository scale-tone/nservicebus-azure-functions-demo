using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Billing
{
    public class TestTransactionsHandler :
        IHandleMessages<TestTransactions>
    {
        static readonly ILog log = LogManager.GetLogger<TestTransactionsHandler>();

        public Task Handle(TestTransactions message, IMessageHandlerContext context)
        {
            log.Info($"Billing has received TestTransactions, Id = {message.OrderId}");
            return Task.CompletedTask;
        }
    }
}