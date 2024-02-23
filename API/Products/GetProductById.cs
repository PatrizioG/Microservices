using FastEndpoints;
using MassTransit;

namespace API.Products
{
    public class GetProductById : Endpoint<GetProductByIdRequestDto, GetProductByIdResponseDto>
    {
        private readonly IRequestClient<Common.Contracts.GetProductById> _requestClient;

        public GetProductById(IRequestClient<Common.Contracts.GetProductById> requestClient)
        {
            _requestClient = requestClient;
        }

        public override void Configure()
        {
            Get("products/{Id}");
            AllowAnonymous();
        }

        public override async Task<GetProductByIdResponseDto> ExecuteAsync(GetProductByIdRequestDto req, CancellationToken cancellationToken)
        {
            var response = await _requestClient.GetResponse<Common.Contracts.ProductByIdResult>(new { }, cancellationToken);
            return new GetProductByIdResponseDto { };
        }
    }
}
