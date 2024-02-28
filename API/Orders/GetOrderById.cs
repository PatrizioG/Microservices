using API.CommonDtos;
using API.Mapper;
using FastEndpoints;
using MassTransit;

namespace API.Orders;

public class GetOrderById : Endpoint<GetOrderByIdRequestDto, OrderDto>
{
    private readonly IRequestClient<Common.Contracts.GetOrderById> _requestClient;

    public GetOrderById(IRequestClient<Common.Contracts.GetOrderById> requestClient)
    {
        _requestClient = requestClient;
    }

    public override void Configure()
    {
        Get("orders/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetOrderByIdRequestDto req, CancellationToken ct)
    {
        var response =
            await _requestClient.GetResponse<Common.Contracts.Order>(new Common.Contracts.GetOrderById { Id = req.Id! },
                ct);
        
        await SendAsync(OrderMapper.MapOrder(response.Message), cancellation: ct);
    }
}