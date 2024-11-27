using Microsoft.AspNetCore.Mvc.Razor;

namespace TestTask.UI.Models;

public class CalculationViewModel
{
    public double GrossAnnualSalary { get; init; }

    public double GrossMonthlySalary { get; init; }

    public double NetAnnualSalary { get; init; }

    public double NetMonthlySalary { get; init; }

    public double AnnualTaxPaid { get; init; }

    public double MonthlyTaxPaid { get; init; }
}
