using FastEndpoints;
using MassTransit;
using MassTransit.Transports;

namespace API.Products
{
    public class List : EndpointWithoutRequest
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public List(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public override void Configure()
        {
            Get("products");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish<HelloMessage>(new HelloMessage { Name = "pippo" });
            await SendAsync(new { Status = "ok"});
        }
    }

    public record HelloMessage
    {
        public string Name { get; init; }
    }
}
