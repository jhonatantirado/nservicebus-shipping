using NServiceBus;

namespace Billing.Contracts.Events
{
    public class OrderBilledEvent : IEvent
    {
        public string OrderId { get; set; }
    }
}
