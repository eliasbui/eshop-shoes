using Eshop.Core.Domain.Bases;
using Eshop.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Infrastructure.EfCore;

public class AppDbContextBase : DbContext, IDomainEventContext
{
    protected AppDbContextBase(DbContextOptions options) : base(options)
    {
    }

    public IEnumerable<EventBase> GetDomainEvents()
    {
        var domainEntities = ChangeTracker
            .Entries<EntityRootBase>()
            .Where(x =>
                x.Entity.DomainEvents.Count != 0)
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ForEach(entity => entity.Entity.DomainEvents.Clear());

        return domainEvents;
    }
}