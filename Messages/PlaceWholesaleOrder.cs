using NServiceBus;

namespace Messages
{
    public class PlaceWholesaleOrder : ICommand
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
    }
}