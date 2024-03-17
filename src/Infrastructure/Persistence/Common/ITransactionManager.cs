using Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Persistence.Common;

public class TransactionManager : ITransactionManager
{
    private readonly DbContext _transactionDbContext;

    public TransactionManager(ApplicationDbContext transactionDbContext)
    {
        _transactionDbContext = transactionDbContext;
    }

    public bool IsCommitted { get; private set; }
    public IDbContextTransaction Transaction { get; private set; }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        var task = await _transactionDbContext.Database.BeginTransactionAsync(
            cancellationToken: cancellationToken
        );
        Transaction = task;
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        await Transaction.CommitAsync(cancellationToken: cancellationToken);
        IsCommitted = true;
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (IsCommitted)
            await Transaction.RollbackAsync(cancellationToken: cancellationToken);
    }

    public async Task DisposeAsync()
    {
        await Transaction.DisposeAsync();
    }
}
