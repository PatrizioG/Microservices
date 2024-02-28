using API.CommonDtos;
using API.Mapper;
using FastEndpoints;
using MassTransit;

namespace API.Products;

public class GetProductById : Endpoint<GetProductByIdRequestDto, ProductDto>
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

    public override async Task<ProductDto> ExecuteAsync(GetProductByIdRequestDto req,
        CancellationToken cancellationToken)
    {
        var response =
            await _requestClient.GetResponse<Common.Contracts.Product>(
                new Common.Contracts.GetProductById { Id = req.Id }, cancellationToken);

        return ProductMapper.MapProduct(response.Message);
    }
}