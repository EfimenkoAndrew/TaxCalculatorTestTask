using MediatR;
using TestTask.Core.Common;
using TestTask.Persistence.CalculationsDb;

namespace TestTask.Infrastructure.Processing;

internal class DomainEventsDispatcher(
    CalculationsDbContext dbContext,
    IMediator mediator) : IDomainEventsDispatcher
{
    public async Task DispatchEventsAsync(CancellationToken cancellationToken)
    {
        var domainEntities = dbContext.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents.Any()).ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            if (cancellationToken.IsCancellationRequested) break;

            await mediator.Publish(domainEvent, cancellationToken);
        }
    }
}
