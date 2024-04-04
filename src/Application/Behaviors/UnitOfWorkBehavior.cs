using Application.Common.Interfaces.Persistence;
using MediatR;

namespace Application.Behaviors;

public class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ITransactionManager _transactionManager;

    public UnitOfWorkBehavior(ITransactionManager transactionManager)
    {
        _transactionManager = transactionManager;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        try
        {
            await _transactionManager.BeginTransactionAsync(cancellationToken: cancellationToken);
            var response = await next();
            await _transactionManager.CommitTransactionAsync(cancellationToken);

            return response;
        }
        catch (Exception)
        {
            await _transactionManager.RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            await _transactionManager.DisposeAsync();
        }
    }
}
