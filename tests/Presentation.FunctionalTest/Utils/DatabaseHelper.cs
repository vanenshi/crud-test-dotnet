using Application.Common.Interfaces.Persistence;
using Domain.Common.Interfaces;
using Infrastructure.Persistence;
using Xunit;

namespace Presentation.FunctionalTest.Utils;

public class DatabaseHelper : IAsyncLifetime
{
    private readonly DatabaseResetter _databaseResetter;
    private readonly IServiceScopeFactory _scopeFactory;

    public DatabaseHelper(CustomWebApplicationFactory factory)
    {
        var applicationDbContext = factory.Services.GetService<ApplicationDbContext>()!;
        _databaseResetter = new DatabaseResetter(applicationDbContext: applicationDbContext);
        _scopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();
    }

    public async Task InitializeAsync()
    {
        await _databaseResetter.ResetDatabase();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class, IEntity
    {
        using var scope = _scopeFactory.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IRepository<TEntity>>();

        await repository.AddAsync(entity);
    }

    public async Task<TEntity?> FindAsync<TEntity>(object?[]? keyValues)
        where TEntity : class, IEntity
    {
        using var scope = _scopeFactory.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IRepository<TEntity>>();

        return await repository.FindAsync(keyValues);
    }
}
