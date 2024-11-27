using MediatR;

namespace TestTask.Application.Domain.Calculations.Commands.CreateCalculation;

public record CreateCalculationCommand(decimal GrossAnnualSalary) : IRequest<CreateCalculationCommandDto>;
