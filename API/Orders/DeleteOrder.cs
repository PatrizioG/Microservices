using FastEndpoints;

namespace API.Orders
{
    public class DeleteOrder : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Delete("orders/{Id}");
            AllowAnonymous();
        }
        public override Task HandleAsync(CancellationToken ct)
        {
            return base.HandleAsync(ct);
        }
    }
}
