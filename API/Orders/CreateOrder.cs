using FastEndpoints;

namespace API.Orders
{
    public class CreateOrder : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Post("orders");
            AllowAnonymous();
        }

        public override Task HandleAsync(CancellationToken ct)
        {
            return base.HandleAsync(ct);
        }
    }
}
