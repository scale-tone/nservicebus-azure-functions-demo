using NServiceBus;

namespace Messages
{
    public class TestDedup : ICommand
    {
        public string OrderId { get; set; }
    }
}