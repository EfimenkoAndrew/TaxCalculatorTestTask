namespace TestTask.Application.Domain.Calculations.Queries.GetCalculationDetails;

public record CalculationDetailsDto(
    Guid Id,
    decimal GrossAnnualSalary,
    decimal GrossMonthlySalary,
    decimal NetAnnualSalary,
    decimal NetMonthlySalary,
    decimal AnnualTaxPaid,
    decimal MonthlyTaxPaid);
