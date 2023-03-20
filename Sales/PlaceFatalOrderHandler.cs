using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Sales
{    
    public class PlaceFatalOrderHandler :
        IHandleMessages<PlaceFatalOrder>
    {
        static readonly ILog log = LogManager.GetLogger<PlaceFatalOrderHandler>();

        public async Task Handle(PlaceFatalOrder message, IMessageHandlerContext context)
        {
            log.Info($"Received PlaceFatalOrder, OrderId = {message.OrderId}");

            throw new Exception($"Got fatal order {message.OrderId}");
        }
    }
}
