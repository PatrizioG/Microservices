using MassTransit;

namespace Products.Consumers
{
    internal class GetProductByIdConsumerDefinition : ConsumerDefinition<GetProductByIdConsumer>
    {
        protected override void ConfigureConsumer(
            IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<GetProductByIdConsumer> consumerConfigurator,
            IRegistrationContext context)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        }
    }
}
