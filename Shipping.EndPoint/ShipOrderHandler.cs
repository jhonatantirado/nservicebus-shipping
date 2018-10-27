using NServiceBus;
using NServiceBus.Logging;
using Shipping.Contracts.Commands;
using System.Threading.Tasks;

namespace Shipping.EndPoint
{
    public class ShipOrderHandler : IHandleMessages<ShipOrderCommand>
    {
        static ILog log = LogManager.GetLogger<ShipOrderHandler>();

        public Task Handle(ShipOrderCommand message, IMessageHandlerContext context)
        {
            log.Info($"Order [{message.OrderId}] - Succesfully shipped.");
            return Task.CompletedTask;
        }
    }
}
