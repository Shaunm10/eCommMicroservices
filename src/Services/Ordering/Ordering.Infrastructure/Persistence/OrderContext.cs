using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence;

public class OrderContext : DbContext
{
    public OrderContext(DbContextOptions<OrderContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var currentUserName = "sms";

        // when saving we want to set the modified dateTime.
        foreach (var entry in this.ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedUtcDate = DateTime.UtcNow;
                    entry.Entity.CreatedBy = currentUserName;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedUtcDate = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = currentUserName;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}