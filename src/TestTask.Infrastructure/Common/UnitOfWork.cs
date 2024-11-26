using TestTask.Core.Common;
using TestTask.Persistence.CalculationsDb;

namespace TestTask.Infrastructure.Common;

public class UnitOfWork(CalculationsDbContext dbContext, IDomainEventsDispatcher domainEventsDispatcher) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await domainEventsDispatcher.DispatchEventsAsync(cancellationToken);
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}
