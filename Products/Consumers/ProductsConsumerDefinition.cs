namespace Company.Consumers
{
    using MassTransit;

    internal class ProductsConsumerDefinition :
        ConsumerDefinition<ProductsConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<ProductsConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        }
    }
}