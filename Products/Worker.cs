using Common.Contracts;
using MassTransit;

namespace Products;

internal class Worker : BackgroundService
{
    readonly IBus _bus;

    public Worker(IBus bus)
    {
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _bus.Publish(new GetAllProducts(), stoppingToken);
            await Task.Delay(1000, stoppingToken);
        }
    }
}
