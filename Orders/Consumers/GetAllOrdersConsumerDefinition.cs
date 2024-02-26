using MassTransit;

namespace Orders.Consumers;

public class GetAllOrdersConsumerDefinition : ConsumerDefinition<GetAllOrdersConsumer>
{
    protected override void ConfigureConsumer(
       IReceiveEndpointConfigurator endpointConfigurator,
       IConsumerConfigurator<GetAllOrdersConsumer> consumerConfigurator,
       IRegistrationContext context)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
    }
}
