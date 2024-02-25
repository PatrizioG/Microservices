using FastEndpoints;

namespace API.Orders
{
    public class UpdateOrder : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Patch("orders");
            AllowAnonymous();
        }

        public override Task HandleAsync(CancellationToken ct)
        {
            return base.HandleAsync(ct);
        }
    }
}
