using FluentAssertions;
using TestTask.Core.Domain.Calculations.Data;
using TestTask.Core.Domain.Calculations.Events;
using TestTask.Core.Domain.Calculations.Models;
using ValidationException = FluentValidation.ValidationException;

namespace TestTasks.Tests.Unit.Domain.Calculations.CalculationsTests;

public class CreateCalculationTests
{
    [Fact]
    public void Should_set_correct_data()
    {
        // Arrange
        var grossAnnualSalary = 10_000m;
        var data = new CreateCalculationData(grossAnnualSalary);

        // Act
        var calculation = Calculation.Create(data);

        // Assert
        calculation.Id.Should().NotBeEmpty();
        calculation.GrossAnnualSalary.Should().Be(grossAnnualSalary);
        calculation.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(3));
    }

    [Fact]
    public void When_created_Should_produse_event()
    {
        // Arrange
        var grossAnnualSalary = 10_000m;
        var data = new CreateCalculationData(grossAnnualSalary);

        // Act
        var calculation = Calculation.Create(data);

        // Assert
        calculation.DomainEvents.Should().ContainSingle(e => e is CalculationCreatedDomainEvent);

        var @event = calculation.DomainEvents.Single(e => e is CalculationCreatedDomainEvent) as CalculationCreatedDomainEvent;
        @event!.Id.Should().Be(calculation.Id);
    }

    [Fact]
    public void When_gross_less_than_zero_Should_throw_exception()
    {
        // Arrange
        var grossAnnualSalary = -1m;
        var data = new CreateCalculationData(grossAnnualSalary);

        // Act
        var act = () => Calculation.Create(data);

        // Assert
        var validationException = act.Should()
            .Throw<ValidationException>()
            .Subject
            .Single();

        var failure = validationException.Errors.Single();
        failure.PropertyName.Should().Be(nameof(CreateCalculationData.GrossAnnualSalary));
        failure.ErrorMessage.Should().Be($"{nameof(CreateCalculationData.GrossAnnualSalary)} must be greater than 0. {nameof(CreateCalculationData.GrossAnnualSalary)}: '{grossAnnualSalary}'.");
    }
}
