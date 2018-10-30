using NServiceBus;
using NServiceBus.Logging;
using Shipping.Contracts.Commands;
using System.Threading.Tasks;

namespace Shipping.EndPoint
{
    public class ShipOrderHandler : IHandleMessages<ShipOrderCommand>
    {
        static ILog log = LogManager.GetLogger<ShipOrderHandler>();
        static readonly string sender = "jhonatantiradotiradodeep@gmail.com";
        static readonly string receiver = "jhonatan.tirado@unmsm.edu.pe";

        public Task Handle(ShipOrderCommand message, IMessageHandlerContext context)
        {
            log.Info($"Order [{message.OrderId}] - Succesfully shipped.");
            SendGridEmail.Submit(sender, receiver, $"Order [{message.OrderId}] - Succesfully shipped.");
            return Task.CompletedTask;
        }
    }
}
