using Common.Contracts;
using FastEndpoints;
using MassTransit;

namespace API.Orders
{
    public class CreateOrder : Endpoint<CreateOrderRequestDto>
    {
        private readonly IRequestClient<Common.Contracts.CreateOrder> _requestClient;

        public CreateOrder(IRequestClient<Common.Contracts.CreateOrder> requestClient)
        {
            _requestClient = requestClient;
        }

        public override void Configure()
        {
            Post("orders");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateOrderRequestDto req, CancellationToken ct)
        {
            var response = await _requestClient.GetResponse<CreateOrderResult>(new Common.Contracts.CreateOrder { UserId = req.UserId!, ProductIds = req.ProductIds }, ct);
            // TODO map dto
            await SendAsync(response.Message);
        }
    }
}
