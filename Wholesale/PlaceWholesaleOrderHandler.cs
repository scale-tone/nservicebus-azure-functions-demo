using System;
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
            // To demonstrate that order is preserved even if a message gets retried
            if (Random.Shared.Next() % 2 == 0)
            {
                throw new Exception("Oops");
            }

            log.Info($"Wholesale has received PlaceWholesaleOrder for customer {message.CustomerId} with Id {message.Id}");
            return Task.CompletedTask;
        }
    }
}