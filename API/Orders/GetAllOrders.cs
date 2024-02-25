using FastEndpoints;

namespace API.Orders
{
    public class GetAllOrders : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Get("orders");
            AllowAnonymous();
        }

        public override Task HandleAsync(CancellationToken ct)
        {
            return base.HandleAsync(ct);
        }
    }
}
