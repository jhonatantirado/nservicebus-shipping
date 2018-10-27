using Billing.Contracts.Events;
using NServiceBus;
using NServiceBus.Logging;
using Sales.Contracts.Events;
using Shipping.Contracts.Commands;
using System.Threading.Tasks;

namespace Shipping.EndPoint
{
    public class ShippingSagaPolicy :
        Saga<ShippingSagaPolicyData>,
        IAmStartedByMessages<OrderPlacedEvent>,
        IAmStartedByMessages<OrderBilledEvent>
    {
        static ILog log = LogManager.GetLogger<ShippingSagaPolicy>();

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ShippingSagaPolicyData> mapper)
        {
            mapper.ConfigureMapping<OrderPlacedEvent>(message => message.OrderId)
                .ToSaga(sagaData => sagaData.OrderId);
            mapper.ConfigureMapping<OrderBilledEvent>(message => message.OrderId)
                .ToSaga(sagaData => sagaData.OrderId);
        }

        public Task Handle(OrderPlacedEvent message, IMessageHandlerContext context)
        {
            log.Info($"OrderPlaced message received, OrderId = {message.OrderId}");
            Data.IsOrderPlaced = true;
            return ProcessOrder(context);
        }

        public Task Handle(OrderBilledEvent message, IMessageHandlerContext context)
        {
            log.Info($"OrderBilled message received, OrderId = {message.OrderId}");
            Data.IsOrderBilled = true;
            return ProcessOrder(context);
        }

        private async Task ProcessOrder(IMessageHandlerContext context)
        {
            if (Data.IsOrderPlaced && Data.IsOrderBilled)
            {
                await context.SendLocal(new ShipOrderCommand() { OrderId = Data.OrderId });
                MarkAsComplete();
            }
        }
    }
}
