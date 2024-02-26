using MassTransit;

namespace Orders.Consumers
{
    public class CreateOrderConsumerDefinition : ConsumerDefinition<CreateOrderConsumer>
    {
        protected override void ConfigureConsumer(
            IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<CreateOrderConsumer> consumerConfigurator,
            IRegistrationContext context)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        }
    }
}
