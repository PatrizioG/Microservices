using FastEndpoints;

namespace API.Orders
{
    public class GetOrderById : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Get("orders/{Id}");
            AllowAnonymous();
        }
        public override Task HandleAsync(CancellationToken ct)
        {
            return base.HandleAsync(ct);
        }
    }
}
