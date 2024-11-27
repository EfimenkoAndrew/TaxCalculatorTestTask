using TestTask.Core.Common;

namespace TestTask.Core.Domain.Calculations.Events;

public record CalculationCreated(Guid Id) : IDomainEvent;
