using Common.Contracts;
using FastEndpoints;
using MassTransit;

namespace API.Products
{
    public class GetAllProducts : EndpointWithoutRequest
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IRequestClient<Common.Contracts.GetAllProducts> _requestClient;

        public GetAllProducts(IPublishEndpoint publishEndpoint, IRequestClient<Common.Contracts.GetAllProducts> requestClient)
        {
            _publishEndpoint = publishEndpoint;
            _requestClient = requestClient;
        }

        public override void Configure()
        {
            Get("products");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            // await _publishEndpoint.Publish<GetAllProducts>(new GetAllProducts());
            var response = await _requestClient.GetResponse<AllProductsResult>(new { }, cancellationToken);
            await SendAsync(response.Message);
        }
    }
}
