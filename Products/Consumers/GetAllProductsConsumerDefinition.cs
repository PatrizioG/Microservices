namespace Products.Consumers
{
    using MassTransit;

    internal class GetAllProductsConsumerDefinition : ConsumerDefinition<GetAllProductsConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<GetAllProductsConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        }
    }
}