using API.CommonDtos;
using API.Mapper;
using Common.Contracts;
using FastEndpoints;
using MassTransit;

namespace API.Orders;

public class UpdateOrder : Endpoint<UpdateOrderRequestDto, OrderDto>
{
    private readonly IRequestClient<Common.Contracts.UpdateOrder> _requestClientForUpdate;
    private readonly IRequestClient<Common.Contracts.GetOrderById> _requestClientForGetOrder;

    public UpdateOrder(
        IRequestClient<Common.Contracts.UpdateOrder> requestClientForUpdate,
        IRequestClient<Common.Contracts.GetOrderById> requestClientForGetOrder)
    {
        _requestClientForUpdate = requestClientForUpdate;
        _requestClientForGetOrder = requestClientForGetOrder;
    }

    public override void Configure()
    {
        Patch("orders");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateOrderRequestDto req, CancellationToken ct)
    {
        await _requestClientForUpdate.GetResponse<GenericResult>(new Common.Contracts.UpdateOrder
        {
            OrderId = req.OrderId,
            OrderLines = req.OrderLines.Select(l => new UpdateOrderLine
            {
                OrderLineId = l.OrderLineId,
                Quantity = l.Quantity
            }).ToList()
        }, ct);

        var response = await _requestClientForGetOrder.GetResponse<Common.Contracts.Order>(
            new Common.Contracts.GetOrderById { Id = req.OrderId! },
            ct);

        await SendAsync(OrderMapper.MapOrder(response.Message), cancellation: ct);
    }
}