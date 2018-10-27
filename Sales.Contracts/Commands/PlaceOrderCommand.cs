using NServiceBus;

namespace Sales.Contracts.Commands
{
    public class PlaceOrderCommand : ICommand
    {
        public string OrderId { get; set; }
        public string OrderData { get; set; }
    }
}
