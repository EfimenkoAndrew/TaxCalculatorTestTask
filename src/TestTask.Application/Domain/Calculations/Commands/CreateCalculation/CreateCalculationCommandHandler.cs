using MediatR;
using TestTask.Core.Common;
using TestTask.Core.Domain.Calculations.Common;
using TestTask.Core.Domain.Calculations.Data;
using TestTask.Core.Domain.Calculations.Models;

namespace TestTask.Application.Domain.Calculations.Commands.CreateCalculation;

public class CreateCalculationCommandHandler(
    ICalculationsRepository calculationsRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateCalculationCommand, CreateCalculationCommandDto>
{
    public async Task<CreateCalculationCommandDto> Handle(CreateCalculationCommand command, CancellationToken cancellationToken)
    {
        var data = new CreateCalculationData(command.GrossAnnualSalary);
        var calculation = Calculation.Create(data);

        calculationsRepository.Add(calculation);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var createCalculationCommandDto = new CreateCalculationCommandDto(
            calculation.Id,
            calculation.GrossAnnualSalary,
            calculation.GrossMonthlySalary,
            calculation.NetAnnualSalary,
            calculation.NetMonthlySalary,
            calculation.AnnualTaxPaid,
            calculation.MonthlyTaxPaid);

        return createCalculationCommandDto;
    }
}
