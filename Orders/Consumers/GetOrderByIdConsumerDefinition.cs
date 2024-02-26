using MassTransit;

namespace Orders.Consumers
{
    public class GetOrderByIdConsumerDefinition : ConsumerDefinition<GetOrderByIdConsumer>
    {
        protected override void ConfigureConsumer(
           IReceiveEndpointConfigurator endpointConfigurator,
           IConsumerConfigurator<GetOrderByIdConsumer> consumerConfigurator,
           IRegistrationContext context)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        }
    }
}
