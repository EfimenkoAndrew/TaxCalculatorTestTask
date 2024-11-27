namespace TestTask.Application.Domain.Calculations.Commands.CreateCalculation;

public record CreateCalculationCommandDto(
    Guid Id,
    decimal GrossAnnualSalary,
    decimal GrossMonthlySalary,
    decimal NetAnnualSalary,
    decimal NetMonthlySalary,
    decimal AnnualTaxPaid,
    decimal MonthlyTaxPaid);
