using API.CommonDtos;
using API.Mapper;
using FastEndpoints;
using MassTransit;

namespace API.Orders;

public class GetAllOrders : EndpointWithoutRequest<List<OrderDto>>
{
    private readonly IRequestClient<Common.Contracts.GetAllOrders> _requestClient;

    public GetAllOrders(IRequestClient<Common.Contracts.GetAllOrders> requestClient)
    {
        _requestClient = requestClient;
    }

    public override void Configure()
    {
        Get("orders");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await _requestClient.GetResponse<Common.Contracts.OrdersResult>(new {}, ct);
        List<OrderDto> result = [];

        foreach (var order in response.Message.Orders)
        {
            result.Add(OrderMapper.MapOrder(order));
        }

        await SendAsync(result);
    }
}
