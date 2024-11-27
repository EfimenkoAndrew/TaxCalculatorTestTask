using MediatR;
using TestTask.Core.Common;
using TestTask.Core.Domain.Calculations.Common;
using TestTask.Core.Domain.Calculations.Data;
using TestTask.Core.Domain.Calculations.Models;

namespace TestTask.Application.Domain.Calculations.Commands.CreateCalculation;

public class CreateCalculationCommandHandler(
    ICalculationsRepository calculationsRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateCalculationCommand, Guid>
{
    public async Task<Guid> Handle(CreateCalculationCommand command, CancellationToken cancellationToken)
    {
        var data = new CreateCalculationData(command.GrossAnnualSalary);
        var calculation = Calculation.Create(data);

        calculationsRepository.Add(calculation);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return calculation.Id;
    }
}
