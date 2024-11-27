namespace TestTask.Application.Domain.Calculations.Queries.GetCalculations;

public record CalculationsDto(
    Guid Id,
    decimal GrossAnnualSalary,
    decimal GrossMonthlySalary,
    decimal NetAnnualSalary,
    decimal NetMonthlySalary,
    decimal AnnualTaxPaid,
    decimal MonthlyTaxPaid);
