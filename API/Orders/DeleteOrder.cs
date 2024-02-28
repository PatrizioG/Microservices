using FastEndpoints;
using MassTransit;

namespace API.Orders;

public class DeleteOrder : Endpoint<DeleteOrderRequestDto, Common.Contracts.GenericResult>
{
    private readonly IRequestClient<Common.Contracts.DeleteOrder> _requestClient;

    public DeleteOrder(IRequestClient<Common.Contracts.DeleteOrder> requestClient)
    {
        _requestClient = requestClient;
    }

    public override void Configure()
    {
        Delete("orders/{Id}");
        AllowAnonymous();
    }

    public override async Task<Common.Contracts.GenericResult> ExecuteAsync(DeleteOrderRequestDto req, CancellationToken ct)
    {
        var response =
            await _requestClient.GetResponse<Common.Contracts.GenericResult>(
                new Common.Contracts.DeleteOrder { Id = req.Id! }, ct);
        
        return response.Message;
    }
}