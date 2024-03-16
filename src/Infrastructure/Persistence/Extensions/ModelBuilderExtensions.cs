using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Pluralize.NET;

namespace Infrastructure.Persistence.Extensions;

public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Pluralizing table name like Post to Posts or Person to People
    /// </summary>
    /// <param name="modelBuilder"></param>
    public static void AddPluralizingTableNameConvention(this ModelBuilder modelBuilder)
    {
        var pluralizer = new Pluralizer();
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            entityType.SetTableName(pluralizer.Pluralize(tableName));
        }
    }

    /// <summary>
    ///     Set DefaultValueSql for specific property name and type
    /// </summary>
    /// <param name="modelBuilder"></param>
    /// <param name="propertyName">Name of property wants to set DefaultValueSql for</param>
    /// <param name="propertyType">Type of property wants to set DefaultValueSql for </param>
    /// <param name="defaultValueSql">DefaultValueSql like "NEWSEQUENTIALID()"</param>
    public static void AddDefaultValueSqlConvention(
        this ModelBuilder modelBuilder,
        string propertyName,
        Type propertyType,
        string defaultValueSql
    )
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var property = entityType
                .GetProperties()
                .SingleOrDefault(
                    p =>
                        p.Name.Equals(
                            value: propertyName,
                            comparisonType: StringComparison.OrdinalIgnoreCase
                        )
                );
            if (property != null && property.ClrType == propertyType)
                property.SetDefaultValue(defaultValueSql);
        }
    }

    /// <summary>
    ///     Set DeleteBehavior.Restrict by default for relations
    /// </summary>
    /// <param name="modelBuilder"></param>
    public static void AddRestrictDeleteBehaviorConvention(this ModelBuilder modelBuilder)
    {
        var cascadeFKs = modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => fk is { IsOwnership: false, DeleteBehavior: DeleteBehavior.Cascade });
        foreach (var fk in cascadeFKs)
            fk.DeleteBehavior = DeleteBehavior.Restrict;
    }

    /// <summary>
    ///     Dynamically register all Entities that inherit from specific BaseType
    /// </summary>
    /// <param name="modelBuilder"></param>
    /// <param name="assemblies">Assemblies contains Entities</param>
    public static void RegisterAllEntities<TBaseType>(
        this ModelBuilder modelBuilder,
        params Assembly[] assemblies
    )
    {
        var types = assemblies
            .SelectMany(a => a.GetExportedTypes())
            .Where(
                c =>
                    c is { IsClass: true, IsAbstract: false, IsPublic: true }
                    && typeof(TBaseType).IsAssignableFrom(c)
            );

        foreach (var type in types)
            modelBuilder.Entity(type);
    }
}
