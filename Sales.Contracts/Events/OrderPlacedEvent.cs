using NServiceBus;

namespace Sales.Contracts.Events
{
    public class OrderPlacedEvent : IEvent
    {
        public string OrderId { get; set; }
    }
}
