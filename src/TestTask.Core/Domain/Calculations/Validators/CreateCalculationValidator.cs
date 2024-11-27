using FluentValidation;
using TestTask.Core.Domain.Calculations.Data;
using TestTask.Core.Domain.Calculations.Models;

namespace TestTask.Core.Domain.Calculations.Validators;

public class CreateCalculationValidator : AbstractValidator<CreateCalculationData>
{
    public CreateCalculationValidator()
    {
        RuleFor(x => x.GrossAnnualSalary)
            .GreaterThan(0)
            .WithMessage(x => $"{nameof(CreateCalculationData.GrossAnnualSalary)} must be greater than 0. {nameof(CreateCalculationData.GrossAnnualSalary)}: '{x.GrossAnnualSalary}'.");
    }
}
