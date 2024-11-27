using TestTask.Core.Domain.Calculations.Common;
using TestTask.Core.Domain.Calculations.Models;
using TestTask.Persistence.CalculationsDb;

namespace TestTask.Infrastructure.Domain.Calculations;

public class CalculationsRepository(CalculationsDbContext calculationsDbContext) : ICalculationsRepository
{
    public void Add(Calculation calculation)
    {
        calculationsDbContext.Calculations.Add(calculation);
    }
}
