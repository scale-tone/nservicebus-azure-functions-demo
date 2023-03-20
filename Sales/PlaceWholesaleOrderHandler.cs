using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Sales
{
    public class PlaceWholesaleOrderHandler :
        IHandleMessages<PlaceWholesaleOrder>
    {
        static readonly ILog log = LogManager.GetLogger<PlaceWholesaleOrderHandler>();

        public Task Handle(PlaceWholesaleOrder message, IMessageHandlerContext context)
        {
            log.Info($"Sales has received PlaceWholesaleOrder for customer {message.CustomerId} with Id {message.Id}");
            return Task.CompletedTask;
        }
    }
}