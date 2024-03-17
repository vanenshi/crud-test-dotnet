using System.Linq.Expressions;
using Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces.Persistence;

public interface IRepository<TEntity>
    where TEntity : class, IEntity
{
    IQueryable<TEntity> Query { get; }
    IQueryable<TEntity> NoTrackingQuery { get; }

    // Find ----------------------------------------

    Task<TEntity?> FindAsync(object?[]? keyValues, CancellationToken cancellationToken = default);

    // Add -----------------------------------------

    Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default,
        bool saveNow = true
    );
    Task AddRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default,
        bool saveNow = true
    );

    // Delete -----------------------------------------

    Task DeleteAsync(
        TEntity entity,
        CancellationToken cancellationToken = default,
        bool saveNow = true
    );
    Task DeleteRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default,
        bool saveNow = true
    );

    // Update -----------------------------------------

    Task UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default,
        bool saveNow = true
    );
    Task UpdateRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default,
        bool saveNow = true
    );
}
