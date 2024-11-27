using TestTask.Core.Domain.Calculations.Models;

namespace TestTask.Core.Domain.Calculations.Common;

public interface ICalculationsRepository
{
    public void Add(Calculation calculation);
}
