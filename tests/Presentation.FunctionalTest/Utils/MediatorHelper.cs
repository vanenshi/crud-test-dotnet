using MediatR;

namespace Presentation.FunctionalTest.Utils;

public class MediatorHelper
{
    private readonly IServiceScopeFactory _scopeFactory;

    public MediatorHelper(CustomWebApplicationFactory factory)
    {
        var scopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();
        _scopeFactory = scopeFactory;
    }

    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        return await mediator.Send(request);
    }

    public async Task SendAsync(IBaseRequest request)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        await mediator.Send(request);
    }
}
