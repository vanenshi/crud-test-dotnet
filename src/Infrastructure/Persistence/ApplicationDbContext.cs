using Domain.Common.Interfaces;
using Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = typeof(IEntity).Assembly;
        modelBuilder.RegisterAllEntities<IEntity>(assembly);
        modelBuilder.AddRestrictDeleteBehaviorConvention();
        modelBuilder.AddPluralizingTableNameConvention();

        var configureAssembly = typeof(ApplicationDbContext).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(configureAssembly);

        base.OnModelCreating(modelBuilder);
    }
}