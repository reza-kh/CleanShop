using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common;

public static class ModelBuilderExtensions
{
    /// <summary>
    /// Automatically registers all non-abstract classes implementing TInterface as EF Core entities.
    /// </summary>
    /// <typeparam name="TInterface">The interface type to scan for (e.g., IEntity)</typeparam>
    /// <param name="modelBuilder">The EF Core ModelBuilder</param>
    /// <param name="assemblies">Assemblies to scan for entities</param>
    public static void RegisterAllEntities<TEntityInterface>(this ModelBuilder modelBuilder)
    {


        var domainAssembly = typeof(TEntityInterface).Assembly;

        var entityTypes = domainAssembly
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && typeof(TEntityInterface).IsAssignableFrom(t))
            .ToList();

        foreach (var type in entityTypes)
        {
            modelBuilder.Entity(type);
        }
    }


    /// <summary>
    /// Apply all IEntityTypeConfiguration<T> classes from multiple assemblies
    /// </summary>
    public static void ApplyConfigurationsFromAssemblies(this ModelBuilder modelBuilder, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}
