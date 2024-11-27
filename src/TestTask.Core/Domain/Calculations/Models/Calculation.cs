using TestTask.Core.Common;
using TestTask.Core.Domain.Calculations.Data;
using TestTask.Core.Domain.Calculations.Events;
using TestTask.Core.Domain.Calculations.Validators;

namespace TestTask.Core.Domain.Calculations.Models;

public class Calculation : Entity
{
    private Calculation()
    {
    }

    private Calculation(
        Guid id,
        decimal grossAnnualSalary,
        decimal grossMonthlySalary,
        decimal netAnnualSalary,
        decimal netMonthlySalary,
        decimal annualTaxPaid,
        decimal monthlyTaxPaid
        )
    {
        Id = id;
        GrossAnnualSalary = grossAnnualSalary;
        GrossMonthlySalary = grossMonthlySalary;
        NetAnnualSalary = netAnnualSalary;
        NetMonthlySalary = netMonthlySalary;
        AnnualTaxPaid = annualTaxPaid;
        MonthlyTaxPaid = monthlyTaxPaid;
    }

    public Guid Id { get; private set; }

    public decimal GrossAnnualSalary { get; private set; }

    public decimal GrossMonthlySalary { get; private set; }

    public decimal NetAnnualSalary { get; private set; }

    public decimal NetMonthlySalary { get; private set; }

    public decimal AnnualTaxPaid { get; private set; }

    public decimal MonthlyTaxPaid { get; private set; }

    public static Calculation Create(CreateCalculationData data)
    {
        // validation
        Validate(new CreateCalculationValidator(), data);

        // calculation
        var grossMonthlySalary = data.GrossAnnualSalary / 12;
        var annualTaxPaid = CalculateAnnualTaxPaid(data.GrossAnnualSalary);
        var monthlyTaxPaid = annualTaxPaid / 12;
        var netAnnualSalary = data.GrossAnnualSalary - annualTaxPaid;
        var netMonthlySalary = netAnnualSalary / 12;

        var calculation = new Calculation(
            Guid.NewGuid(),
            data.GrossAnnualSalary,
            grossMonthlySalary,
            netAnnualSalary,
            netMonthlySalary,
            annualTaxPaid,
            monthlyTaxPaid
        );

        // notify
        calculation.AddDomainEvent(new CalculationCreated(calculation.Id));

        return calculation;
    }

    private static decimal CalculateAnnualTaxPaid(decimal grossAnnualSalary)
    {
        // band A - 0 - 5,000 -> 0%
        var bandA = 5000;
        if (grossAnnualSalary < bandA) return grossAnnualSalary;

        // band B - 5,000 - 20,000 -> 20%
        var bandB = 20000;
        grossAnnualSalary -= bandA;
        if (grossAnnualSalary < bandB) return grossAnnualSalary * 0.20m;

        // band C - 20,001 and up -> 40%
        var bandC = 20000;
        grossAnnualSalary -= bandC;
        return bandA * 0 + bandB * 0.20m + grossAnnualSalary * 0.40m;
    }
}
