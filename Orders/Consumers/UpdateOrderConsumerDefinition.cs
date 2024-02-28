using MassTransit;

namespace Orders.Consumers;

public class UpdateOrderConsumerDefinition :  ConsumerDefinition<UpdateOrderConsumer>
{
    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<UpdateOrderConsumer> consumerConfigurator,
        IRegistrationContext context)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
    }
}