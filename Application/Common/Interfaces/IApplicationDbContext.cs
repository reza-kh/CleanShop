using Application.Common.Behaviors;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class, IEntity;

    Task<OperationResult> SaveChangesAsync(CancellationToken cancellationToken);
    OperationResult SaveChanges();
}
