using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Shipping
{
    public class TestTransactionsHandler :
        IHandleMessages<TestTransactions>
    {
        static readonly ILog log = LogManager.GetLogger<TestTransactionsHandler>();

        public Task Handle(TestTransactions message, IMessageHandlerContext context)
        {
            log.Info($"Shipping has received TestTransactions, Id = {message.OrderId}");
            return Task.CompletedTask;
        }
    }
}