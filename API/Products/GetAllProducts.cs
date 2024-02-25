using API.Mapper;
using Common.Contracts;
using FastEndpoints;
using MassTransit;

namespace API.Products
{
    public class GetAllProducts : EndpointWithoutRequest<GetAllProductsResponseDto>
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
            var response = await _requestClient.GetResponse<GetAllProductsResult>(new { }, cancellationToken);

            await SendAsync(new GetAllProductsResponseDto
            {
                Products = response.Message.Products
                .Select(x => ProductMapper.MapProduct(x))
                .ToList()
            });
        }
    }
}
