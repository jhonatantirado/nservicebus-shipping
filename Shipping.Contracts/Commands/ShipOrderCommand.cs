using NServiceBus;

namespace Shipping.Contracts.Commands
{
    public class ShipOrderCommand : ICommand
    {
        public string OrderId { get; set; }
    }
}
