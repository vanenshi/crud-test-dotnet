using System.Linq.Expressions;
using Application.Common.Interfaces.Persistence;
using Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Common;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
{
    private readonly ApplicationDbContext _dbContext;

    public Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        Entities = _dbContext.Set<TEntity>();
    }

    private DbSet<TEntity> Entities { get; }
    public virtual IQueryable<TEntity> Query => Entities;
    public virtual IQueryable<TEntity> NoTrackingQuery => Entities.AsNoTracking();

    #region Add
    public async Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default,
        bool saveNow = true
    )
    {
        await Entities
            .AddAsync(entity: entity, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        _dbContext.Database.GetConnectionString();

        if (saveNow)
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task AddRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default,
        bool saveNow = true
    )
    {
        await Entities
            .AddRangeAsync(entities: entities, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        if (saveNow)
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
    #endregion
    #region Delete
    public async Task DeleteAsync(
        TEntity entity,
        CancellationToken cancellationToken = default,
        bool saveNow = true
    )
    {
        Entities.Remove(entity);
        if (saveNow)
            await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default,
        bool saveNow = true
    )
    {
        Entities.RemoveRange(entities);
        if (saveNow)
            await _dbContext.SaveChangesAsync(cancellationToken);
    }
    #endregion
    #region Update
    public async Task UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default,
        bool saveNow = true
    )
    {
        Entities.Update(entity);
        if (saveNow)
            await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default,
        bool saveNow = true
    )
    {
        Entities.UpdateRange(entities);
        if (saveNow)
            await _dbContext.SaveChangesAsync(cancellationToken);
    }
    #endregion
    #region Find
    public async Task<TEntity?> FindAsync(
        object?[]? keyValues,
        CancellationToken cancellationToken = default
    )
    {
        return await _dbContext.FindAsync<TEntity>(
            keyValues: keyValues,
            cancellationToken: cancellationToken
        );
    }
    #endregion
}
