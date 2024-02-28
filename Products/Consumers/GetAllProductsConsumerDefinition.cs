namespace Products.Consumers;

using MassTransit;

public class GetAllProductsConsumerDefinition : ConsumerDefinition<GetAllProductsConsumer>
{
    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<GetAllProductsConsumer> consumerConfigurator,
        IRegistrationContext context)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
    }
}