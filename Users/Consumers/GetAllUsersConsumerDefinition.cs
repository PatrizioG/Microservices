using MassTransit;

namespace Users.Consumers
{
    public class GetAllUsersConsumerDefinition : ConsumerDefinition<GetAllUsersConsumer>
    {
        protected override void ConfigureConsumer
            (IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<GetAllUsersConsumer> consumerConfigurator,
            IRegistrationContext context)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        }
    }
}
