using NServiceBus;

namespace Messages
{
    public class PlaceFatalOrder : ICommand
    {
        public string OrderId { get; set; }
    }
}