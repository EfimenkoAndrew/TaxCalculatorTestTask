using TestTask.Core.Domain.Calculations.Common;
using TestTask.Core.Domain.Calculations.Models;

namespace TestTask.Infrastructure.Domain.Calculations;

public class CalculationsRepository : ICalculationsRepository
{
    public Task<Calculation> FindAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Calculation>> FindManyAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Add(Calculation calculation)
    {
        throw new NotImplementedException();
    }

    public void Delete(IReadOnlyCollection<Calculation> calculations)
    {
        throw new NotImplementedException();
    }
}
