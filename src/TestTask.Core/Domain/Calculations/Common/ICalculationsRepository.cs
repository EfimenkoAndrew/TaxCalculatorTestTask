using TestTask.Core.Domain.Calculations.Models;

namespace TestTask.Core.Domain.Calculations.Common;

public interface ICalculationsRepository
{
    public Task<Calculation> FindAsync(Guid id, CancellationToken cancellationToken);

    public Task<IReadOnlyCollection<Calculation>> FindManyAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken);

    public void Add(Calculation calculation);

    public void Delete(IReadOnlyCollection<Calculation> calculations);
}