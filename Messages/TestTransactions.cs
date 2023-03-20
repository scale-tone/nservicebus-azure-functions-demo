using NServiceBus;

namespace Messages
{
    public class TestTransactions : ICommand
    {
        public string OrderId { get; set; }
    }
}