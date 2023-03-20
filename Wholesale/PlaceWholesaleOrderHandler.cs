using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Wholesale
{
    public class PlaceWholesaleOrderHandler :
        IHandleMessages<PlaceWholesaleOrder>
    {
        static readonly ILog log = LogManager.GetLogger<PlaceWholesaleOrderHandler>();

        public Task Handle(PlaceWholesaleOrder message, IMessageHandlerContext context)
        {
            log.Info($"Wholesale has received PlaceWholesaleOrder for customer {message.CustomerId} with Id {message.Id}");
            return Task.CompletedTask;
        }
    }
}