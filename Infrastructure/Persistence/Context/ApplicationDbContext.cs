
using Domain;
using Domain.Customers.Entity;
using Domain.Inventory.Entity;
using Domain.Orders.Entity;
using Domain.Products.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Context;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    private readonly ILogger<ApplicationDbContext> _logger;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILogger<ApplicationDbContext> logger)
        : base(options)
    {
        _logger = logger;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    async Task<OperationResult> IApplicationDbContext.SaveChangesAsync(CancellationToken cancellationToken)
    {
        try
        {
            var affected = await base.SaveChangesAsync(cancellationToken);

            if (affected > 0)
                return OperationResult.Success(new List<string> { ApplicationMessages.OperationSuccesfulDone }, affected);

            if (affected == 0)
                return OperationResult.NoChanges(new List<string> { ApplicationMessages.NoChangesDetected }, affected);

            return OperationResult.Failure(new List<string> { ApplicationMessages.OperationFailureDone }, affected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DataBase_Exception");
            return OperationResult.Failure(new List<string> { ApplicationMessages.OperationFailureDone });
        }
    }

    OperationResult IApplicationDbContext.SaveChanges()
    {
        try
        {
            var affected =  base.SaveChanges();

            if (affected > 0)
                return OperationResult.Success(new List<string> { ApplicationMessages.OperationSuccesfulDone }, affected);

            if (affected == 0)
                return OperationResult.NoChanges(new List<string> { ApplicationMessages.NoChangesDetected }, affected);

            return OperationResult.Failure(new List<string> { ApplicationMessages.OperationFailureDone }, affected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DataBase_Exception");
            return OperationResult.Failure(new List<string> { ApplicationMessages.OperationFailureDone });
        }
    }
}
