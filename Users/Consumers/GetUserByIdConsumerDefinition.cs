using MassTransit;

namespace Users.Consumers
{
    public class GetUserByIdConsumerDefinition : ConsumerDefinition<GetUserByIdConsumer>
    {
        protected override void ConfigureConsumer
           (IReceiveEndpointConfigurator endpointConfigurator,
           IConsumerConfigurator<GetUserByIdConsumer> consumerConfigurator,
           IRegistrationContext context)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        }
    }
}
